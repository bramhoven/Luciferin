using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Converters.Helper;
using Luciferin.BusinessLayer.Exceptions;
using Luciferin.BusinessLayer.Firefly;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Import.Mappers;
using Luciferin.BusinessLayer.Import.Models;
using Luciferin.BusinessLayer.Import.Stores;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Luciferin.BusinessLayer.Import;

public abstract class ImportManagerBase : IImportManager
{
    #region Constructors

    protected ImportManagerBase(INordigenManager nordigenManager, IFireflyManager fireflyManager,
        ISettingsManager settingsManager,
        IImportStatisticsStore importStatisticsStore, TransactionMapper transactionMapper, AccountMapper accountMapper,
        ICompositeLogger<IImportManager> logger)
    {
        NordigenManager = nordigenManager;
        FireflyManager = fireflyManager;
        _settingsManager = settingsManager;
        _importStatisticsStore = importStatisticsStore;
        TransactionMapper = transactionMapper;
        AccountMapper = accountMapper;
        Logger = logger;
    }

    #endregion

    #region Fields

    private readonly IImportStatisticsStore _importStatisticsStore;

    private readonly ISettingsManager _settingsManager;

    protected readonly IFireflyManager FireflyManager;

    protected readonly ICompositeLogger<IImportManager> Logger;

    protected readonly INordigenManager NordigenManager;

    protected readonly TransactionMapper TransactionMapper;

    protected readonly AccountMapper AccountMapper;

    #endregion

    #region Properties

    protected PlatformSettings PlatformSettings => _settingsManager.GetPlatformSettings();

    protected static Statistic Statistic { get; set; }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<Requisition> AddNewBank(string institutionId, string name, string redirectUrl)
    {
        var institution = await NordigenManager.GetInstitution(institutionId);
        var endUserAgreement = await NordigenManager.CreateEndUserAgreement(institution);
        return await NordigenManager.CreateRequisition(institution, name, endUserAgreement, redirectUrl);
    }

    /// <inheritdoc />
    public async Task<bool> CheckAndExecuteAutomaticImport(CancellationToken cancellationToken)
    {
        try
        {
            if (await CheckAutomaticImport())
                return true;

            await Logger.LogInformation("Started import job");

            if (!await ExecuteAutomaticImport(cancellationToken))
                return true;

            await Logger.LogInformation("Finished import job successfully");

            return true;
        }
        catch (Exception)
        {
            await Logger.LogError("Import job failed");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBank(string requisitionId)
    {
        try
        {
            var requisition = await NordigenManager.GetRequisition(requisitionId);
            var endUserAgreement = await NordigenManager.GetEndUserAgreement(requisition.Agreement);

            await NordigenManager.DeleteEndUserAgreement(endUserAgreement.Id);
            await NordigenManager.DeleteRequisition(requisition.Id);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<ICollection<Requisition>> GetRequisitions()
    {
        var requisitions = await NordigenManager.GetRequisitions();

        foreach (var requisition in requisitions)
        {
            var removableAccounts = new List<string>();
            foreach (var currentAccount in requisition.Accounts)
                try
                {
                    var accountInfo = await NordigenManager.GetAccount(currentAccount);
                    if (accountInfo.Status == NordigenConstants.AccountSuspended ||
                        accountInfo.Status == NordigenConstants.AccountExpired)
                    {
                        requisition.IsSuspended = true;
                        removableAccounts.Add(currentAccount);
                    }

                    var details = await NordigenManager.GetAccountDetails(currentAccount);
                    if (string.IsNullOrWhiteSpace(details.Iban))
                        removableAccounts.Add(currentAccount);
                }
                catch (AccountSuspendedException)
                {
                    requisition.IsSuspended = true;
                    removableAccounts.Add(currentAccount);
                }

            foreach (var removableAccount in removableAccounts)
                requisition.Accounts.Remove(removableAccount);
        }

        return requisitions;
    }

    /// <inheritdoc />
    public async ValueTask StartImport(IServiceScope scope, CancellationToken cancellationToken)
    {
        try
        {
            await RunImport(cancellationToken);
        }
        finally
        {
            if (Statistic != null)
                LogAndResetImportStatistic();
        }

        scope.Dispose();
    }

    /// <summary>
    ///     Checks for duplicate transfers.
    /// </summary>
    /// <param name="transactions">The new transactions.</param>
    /// <param name="fireflyTransactions">The existing firefly transactions.</param>
    /// <param name="requisitionIbans">The list of requisition ibans.</param>
    /// <returns></returns>
    protected async Task<IEnumerable<FireflyTransaction>> CheckForDuplicateTransfers(
        IEnumerable<FireflyTransaction> transactions,
        ICollection<FireflyTransaction> fireflyTransactions,
        ICollection<string> requisitionIbans)
    {
        await Logger.LogInformation("Checking transactions for duplicates transfers");

        var nonDuplicateTransactions = transactions.Where(t => t.Type != TransactionType.Transfer).ToList();

        var combinedTransactions = transactions.Concat(fireflyTransactions);
        combinedTransactions = combinedTransactions.DistinctBy(t => t.ExternalId);
        var groups = combinedTransactions
            .Where(t => t.Type == TransactionType.Transfer)
            .GroupBy(t => t.GetConsistentHash());
        foreach (var group in groups)
        {
            var possibleDuplicates = group.ToList();
            if (possibleDuplicates.Count == 1)
            {
                nonDuplicateTransactions.Add(possibleDuplicates.FirstOrDefault());
                continue;
            }

            var nonDuplicates = group.Where(
                    t => (requisitionIbans.Contains(t.SourceIban) &&
                          t.SourceIban.Equals(t.RequisitionIban, StringComparison.InvariantCultureIgnoreCase))
                         || (!requisitionIbans.Contains(t.SourceIban) &&
                             t.DestinationIban.Equals(t.RequisitionIban, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();
            if (nonDuplicates.Any())
                nonDuplicateTransactions.AddRange(nonDuplicates);
        }

        nonDuplicateTransactions = nonDuplicateTransactions
            .Where(t => transactions.Contains(t) && !fireflyTransactions.Contains(t)).ToList();

        await Logger.LogInformation($"{nonDuplicateTransactions.Count} transactions left after duplicate check");

        if (Statistic != null)
            Statistic.TransfersFiltered = transactions.Count() - nonDuplicateTransactions.Count;

        return nonDuplicateTransactions;
    }

    /// <summary>
    ///     Creates the import tag to add to the transactions.
    /// </summary>
    /// <returns></returns>
    protected async Task<FireflyTag> CreateImportTag()
    {
        await Logger.LogInformation("Creating the tag to add to the imported transactions");

        var date = DateTime.Now;
        var dateString = date.ToString("HH:mm:ss dd-MM-yyyy");

        var tagString = $"Imported by Firefly III Importer | {dateString}";
        var tagDescription =
            $"Tag for transactions that have been imported by the Firefly III Importer on {dateString}";

        var tag = new FireflyTag
        {
            Date = date,
            Tag = tagString,
            Description = tagDescription
        };

        return tag;
    }

    /// <summary>
    ///     Gets existing Firefly transactions.
    /// </summary>
    /// <returns></returns>
    protected async Task<ICollection<FireflyTransaction>> GetExistingFireflyTransactions()
    {
        await Logger.LogInformation("Getting existing Firefly transactions");

        var transactions = await FireflyManager.GetTransactions();

        await Logger.LogInformation($"Retrieved {transactions.Count} existing Firefly transactions");

        if (Statistic != null)
            Statistic.TotalFireflyTransactions = transactions.Count;

        return transactions;
    }

    /// <summary>
    ///     Gets all the transactions for a requisition.
    /// </summary>
    /// <param name="accountId">The account id for which to get the transactions.</param>
    /// <param name="requisition">The requisition to extend data.</param>
    /// <returns></returns>
    protected async Task<ICollection<Transaction>> GetTransactionForRequisitionAccount(string accountId,
        Requisition requisition)
    {
        var details = await NordigenManager.GetAccountDetails(accountId);

        await Logger.LogInformation($"Getting all transactions for {details.Iban}");

        ICollection<Transaction> transactions;
        var daysToImport = PlatformSettings.ImportDays.Value;
        if (daysToImport > 0)
            transactions =
                await NordigenManager.GetAccountTransactions(accountId, DateTime.Today.AddDays(-daysToImport));
        else
            transactions = await NordigenManager.GetAccountTransactions(accountId);

        foreach (var transaction in transactions)
            ExtendData(transaction, requisition, details);

        await Logger.LogInformation($"Retrieved {transactions.Count} transactions for {details.Iban}");

        if (Statistic != null)
            Statistic.TotalRetrievedTransactions += transactions.Count;

        return transactions;
    }

    /// <summary>
    ///     Gets all the transactions for a requisition from a date.
    /// </summary>
    /// <param name="accountId">The account id for which to get the transactions.</param>
    /// <param name="requisition">The requisition to extend data.</param>
    /// <param name="fromDate">The from date for the export.</param>
    /// <returns></returns>
    protected async Task<ICollection<Transaction>> GetTransactionForRequisitionAccountFromDate(string accountId,
        Requisition requisition, DateTime fromDate)
    {
        var details = await NordigenManager.GetAccountDetails(accountId);

        await Logger.LogInformation($"Getting all transactions for {details.Iban} from {fromDate:dd/MM/yyyy HH:mm:ss}");

        var transactions = await NordigenManager.GetAccountTransactions(accountId, fromDate);
        foreach (var transaction in transactions)
            ExtendData(transaction, requisition, details);

        await Logger.LogInformation($"Retrieved {transactions.Count} transactions for {details.Iban}");

        if (Statistic != null)
            Statistic.TotalRetrievedTransactions += transactions.Count;

        return transactions;
    }

    /// <summary>
    ///     Imports a list of firefly accounts.
    /// </summary>
    /// <param name="accounts">The list of accounts to import.</param>
    /// <returns></returns>
    protected async Task ImportAccounts(ICollection<FireflyAccount> accounts)
    {
        await Logger.LogInformation($"Start importing {accounts.Count} accounts");

        try
        {
            await FireflyManager.AddNewAccounts(accounts);
            await Logger.LogInformation($"Imported {accounts.Count} accounts");
        }
        catch (Exception e)
        {
            await Logger.Log(LogLevel.Error, e, e.Message);
        }
    }

    /// <summary>
    ///     Imports a list of firefly transactions.
    /// </summary>
    /// <param name="fireflyTransactions">The list of transactions to import.</param>
    /// <returns></returns>
    protected async Task ImportTransactions(ICollection<FireflyTransaction> fireflyTransactions)
    {
        await Logger.LogInformation($"Start importing {fireflyTransactions.Count} transactions");

        try
        {
            await FireflyManager.AddNewTransactions(fireflyTransactions);
            await Logger.LogInformation($"Imported {fireflyTransactions.Count} transactions");
        }
        catch (Exception e)
        {
            await Logger.Log(LogLevel.Error, e, e.Message);
        }
    }

    /// <summary>
    ///     Removes the existing transactions from the new transactions list.
    /// </summary>
    /// <param name="newTransactions">The new transactions.</param>
    /// <param name="existingTransactions">The existing transactions.</param>
    /// <returns></returns>
    protected async Task<IEnumerable<FireflyTransaction>> RemoveExistingTransactions(
        IEnumerable<FireflyTransaction> newTransactions,
        ICollection<FireflyTransaction> existingTransactions)
    {
        await Logger.LogInformation("Checking existing Firefly transactions");

        var transactions = newTransactions
            .Where(t => existingTransactions.All(ft =>
                !string.Equals(ft.ExternalId, t.ExternalId, StringComparison.InvariantCultureIgnoreCase)))
            .ToList();

        await Logger.LogInformation($"{transactions.Count} transactions left after existing check");

        if (Statistic != null)
            Statistic.ExistingTransactionsFiltered = newTransactions.Count() - transactions.Count;

        return transactions;
    }

    /// <summary>
    ///     Runs the import.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    protected abstract ValueTask RunImport(CancellationToken cancellationToken);

    /// <summary>
    ///     Runs the import from a date.
    /// </summary>
    /// <param name="fromDate">The from date.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    protected abstract ValueTask RunImport(DateTime fromDate, CancellationToken cancellationToken);

    /// <summary>
    ///     Sets the correct starting balances.
    /// </summary>
    /// <param name="currentBalances">The dictionary with iban and current balance.</param>
    /// <param name="fireflyAccounts">The list of all Firefly accounts.</param>
    protected async Task SetStartingBalances(IDictionary<string, string> currentBalances,
        ICollection<FireflyAccount> fireflyAccounts)
    {
        await Logger.LogInformation("First import detected");
        await Logger.LogInformation("Start setting starting balances");

        foreach (var account in currentBalances)
        {
            var iban = account.Key;
            var bankBalance = decimal.Parse(account.Value, new CultureInfo("en-US"));

            var fireflyAccount = fireflyAccounts.FirstOrDefault(a =>
                string.Equals(a.Iban, iban, StringComparison.InvariantCultureIgnoreCase));
            if (fireflyAccount == null)
                continue;

            var transaction = await FireflyManager.GetFirstTransactionOfAccount(fireflyAccount.Id);

            var currentFireflyBalance = decimal.Parse(fireflyAccount.CurrentBalance, new CultureInfo("en-US"));
            var openingBalance = bankBalance - currentFireflyBalance;

            fireflyAccount.OpeningBalanceDate = transaction.Date.Date.AddDays(-1);
            fireflyAccount.OpeningBalance = openingBalance.ToString("0.00", CultureInfo.InvariantCulture);

            await FireflyManager.UpdateAccount(fireflyAccount);

            await Logger.LogInformation(
                $"[{fireflyAccount.Name}] Set opening balance to: {openingBalance.ToString("0.00", CultureInfo.InvariantCulture)}");
        }

        await Logger.LogInformation("Finished setting starting balances");
    }

    /// <summary>
    ///     Checks whether automatic import can run.
    /// </summary>
    /// <returns></returns>
    private async Task<bool> CheckAutomaticImport()
    {
        if (!PlatformSettings.AutomaticImport.Value)
            return true;

        return false;
    }

    /// <summary>
    ///     Executes the automatic import.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    private async Task<bool> ExecuteAutomaticImport(CancellationToken cancellationToken)
    {
        var lastImport = _importStatisticsStore.GetLastImportDateTime();
        if (lastImport == DateTime.MinValue)
        {
            await Logger.LogWarning("Please manually import first before automatic import can run");
            return false;
        }

        var importFromDate = lastImport.Date.AddDays(-3);
        await RunImport(importFromDate, cancellationToken);
        return true;
    }

    /// <summary>
    ///     Logs and resets the import statistic property.
    /// </summary>
    /// <returns></returns>
    private void LogAndResetImportStatistic()
    {
        try
        {
            _importStatisticsStore.InsertImportStatistic(Statistic);
            Statistic = null;
        }
        catch (Exception e)
        {
            Logger.Log(LogLevel.Error, e.Message);
        }
    }

    #region Static Methods

    /// <summary>
    ///     Extends the transaction data with requisition data.
    /// </summary>
    /// <param name="transaction">The transaction to extend.</param>
    /// <param name="requisition">The requisition.</param>
    /// <param name="details">The requisition account details.</param>
    private static void ExtendData(Transaction transaction, Requisition requisition, AccountDetails details)
    {
        transaction.RequisitorIban = details.Iban;
        transaction.RequisitorBank = BankHelper.GetBankType(requisition.InstitutionId);
    }

    #endregion

    #endregion
}