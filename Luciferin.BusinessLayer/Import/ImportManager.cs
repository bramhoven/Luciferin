using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly;
using Luciferin.BusinessLayer.Import.Mappers;
using Luciferin.BusinessLayer.Import.Models;
using Luciferin.BusinessLayer.Import.Stores;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Settings;

namespace Luciferin.BusinessLayer.Import
{
    public class ImportManager : ImportManagerBase
    {
        #region Constructors

        public ImportManager(INordigenManager nordigenManager,
                             IFireflyManager fireflyManager,
                             ISettingsManager settingsManager,
                             IImportStatisticsStore importStatisticsStore,
                             TransactionMapper transactionMapper,
                             ICompositeLogger<ImportManager> logger) : base(nordigenManager, fireflyManager, settingsManager, importStatisticsStore, transactionMapper, logger) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override ValueTask RunImport(CancellationToken cancellationToken)
        {
            return RunImport(DateTime.MinValue, cancellationToken);
        }

        /// <inheritdoc />
        protected override async ValueTask RunImport(DateTime fromDate, CancellationToken cancellationToken)
        {
            Statistic = new Statistic();

            await Task.Delay(1000, cancellationToken);
            await Logger.LogInformation("Starting import");

            var existingFireflyTransactions = await GetExistingFireflyTransactions();
            var requisitions = await GetRequisitions();

            await Logger.LogInformation($"Start import for {requisitions.Count} connected banks");

            var firstImport = !existingFireflyTransactions.Any();
            var balances = await GetBalances(requisitions);
            var newTransactions = fromDate != DateTime.MinValue ? await GetTransactionsFromDate(requisitions, fromDate) : await GetTransactions(requisitions);

            Statistic.TotalAccounts = requisitions.Sum(r => r.Accounts.Count);

            await Logger.LogInformation($"Retrieved a total of {newTransactions.Count} transactions");

            var accounts = await FireflyManager.GetAccounts();
            var tag = await CreateImportTag();
            Statistic.ImportDate = tag.Date;

            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts, tag.Tag).ToList();
            newFireflyTransactions = (await RemoveExistingTransactions(newFireflyTransactions, existingFireflyTransactions)).ToList();
            newFireflyTransactions = (await CheckForDuplicateTransfers(newFireflyTransactions, existingFireflyTransactions, balances.Keys)).ToList();

            if (!newFireflyTransactions.Any())
            {
                await Logger.LogInformation("No new transactions to import");
                await Logger.LogInformation("Dropped import tag");
                return;
            }

            await FireflyManager.AddNewTag(tag);

            await Logger.LogInformation($"Created the import tag: {tag.Tag}");

            newFireflyTransactions = newFireflyTransactions.Where(t => !string.Equals(t.Amount, "0")).OrderBy(t => t.Date).ToList();
            await ImportTransactions(newFireflyTransactions);
            Statistic.NewTransactions = newFireflyTransactions.Count;

            if (!firstImport)
                return;

            accounts = await FireflyManager.GetAccounts();
            await SetStartingBalances(balances, accounts);

            Statistic.StartingBalanceSet = true;
        }

        /// <summary>
        /// Gets a dictionary of all the balances for the accounts in the requisitions.
        /// </summary>
        /// <param name="requisitions">The requisitions.</param>
        /// <returns></returns>
        private async Task<IDictionary<string, string>> GetBalances(IEnumerable<Requisition> requisitions)
        {
            var balances = new Dictionary<string, string>();
            foreach (var requisition in requisitions)
            {
                foreach (var account in requisition.Accounts)
                {
                    var details = await NordigenManager.GetAccountDetails(account);
                    var balance = await NordigenManager.GetAccountBalance(account);
                    balances.Add(details.Iban, balance.FirstOrDefault()?.BalanceAmount.Amount);
                }
            }

            return balances;
        }

        /// <summary>
        /// Gets all the transactions for a list of requisitions.
        /// </summary>
        /// <param name="requisitions">The requisitions.</param>
        /// <returns></returns>
        private async Task<ICollection<Transaction>> GetTransactions(IEnumerable<Requisition> requisitions)
        {
            var transactions = new List<Transaction>();
            foreach (var requisition in requisitions)
            {
                foreach (var account in requisition.Accounts)
                {
                    transactions.AddRange(await GetTransactionForRequisitionAccount(account, requisition));
                }
            }

            return transactions;
        }

        /// <summary>
        /// Gets all the transactions for a list of requisitions from a specific date.
        /// </summary>
        /// <param name="requisitions">The requisitions.</param>
        /// <param name="fromDate">The from date.</param>
        /// <returns></returns>
        private async Task<ICollection<Transaction>> GetTransactionsFromDate(IEnumerable<Requisition> requisitions, DateTime fromDate)
        {
            var transactions = new List<Transaction>();
            foreach (var requisition in requisitions)
            {
                foreach (var account in requisition.Accounts)
                {
                    transactions.AddRange(await GetTransactionForRequisitionAccountFromDate(account, requisition, fromDate));
                }
            }

            return transactions;
        }

        #endregion
    }
}