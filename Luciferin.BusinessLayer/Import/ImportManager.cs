using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Import.Mappers;
using Luciferin.BusinessLayer.Import.Models;
using Luciferin.BusinessLayer.Import.Processors;
using Luciferin.BusinessLayer.Import.Stores;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Settings;

namespace Luciferin.BusinessLayer.Import
{
    public class ImportManager : ImportManagerBase
    {
        private readonly FilterDuplicateTransactionProcessor _filterDuplicateTransactionProcessor;

        private readonly FilterExistingTransactionProcessor _filterExistingTransactionProcessor;

        #region Constructors

        public ImportManager(INordigenManager nordigenManager,
                             IFireflyManager fireflyManager,
                             ISettingsManager settingsManager,
                             IImportStatisticsStore importStatisticsStore,
                             TransactionMapper transactionMapper,
                             AccountMapper accountMapper,
                             FilterExistingTransactionProcessor filterExistingTransactionProcessor,
                             FilterDuplicateTransactionProcessor filterDuplicateTransactionProcessor,
                             ICompositeLogger<ImportManager> logger) : base(nordigenManager, fireflyManager, settingsManager, importStatisticsStore, transactionMapper, accountMapper,
                                                                            logger)
        {
            _filterExistingTransactionProcessor = filterExistingTransactionProcessor;
            _filterDuplicateTransactionProcessor = filterDuplicateTransactionProcessor;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override ValueTask RunImport(CancellationToken cancellationToken)
        {
            return RunImport(DateTime.MinValue, cancellationToken);
        }

        private async Task<ICollection<FireflyTransaction>> FilterTransactionsWithMethods(ICollection<FireflyTransaction> newTransactions,
                                                                                          ICollection<FireflyTransaction> existingTransactions,
                                                                                          ICollection<string> requisitionIbans)
        {
            newTransactions = (await RemoveExistingTransactions(newTransactions, existingTransactions)).ToList();
            newTransactions = (await CheckForDuplicateTransfers(newTransactions, existingTransactions, requisitionIbans)).ToList();
            return newTransactions;
        }

        private async Task<ICollection<FireflyTransaction>> FilterTransactionsWithProcessDirector(ICollection<FireflyTransaction> newTransactions,
                                                                                                  ICollection<FireflyTransaction> existingTransactions)
        {
            _filterExistingTransactionProcessor.SetExistingTransactions(existingTransactions);
            _filterDuplicateTransactionProcessor.SetExistingTransactions(existingTransactions);

            var director = new ProcessorDirector();
            director.AddProcessor(_filterExistingTransactionProcessor);
            director.AddProcessor(_filterDuplicateTransactionProcessor);

            var results = await director.ProcessTransactions(newTransactions);
            return results.Where(r => r.Status == ProcessedStatus.Success || r.ProcessedTransaction != null)
                          .Select(r => r.ProcessedTransaction)
                          .Cast<FireflyTransaction>()
                          .ToList();
        }

        /// <inheritdoc />
        protected override async ValueTask RunImport(DateTime fromDate, CancellationToken cancellationToken)
        {
            Statistic = new Statistic();

            await Task.Delay(1000, cancellationToken);
            await Logger.LogInformation("Starting import");

            var existingFireflyTransactions = await GetExistingFireflyTransactions(fromDate);
            var requisitions = await GetImportableRequisitions();
            var requisitionAccounts = requisitions.SelectMany(r => r.Accounts.Select(a => NordigenManager.GetAccountDetails(a).Result)).ToList();
            var requisitionIbans = requisitionAccounts.Select(a => a.Iban).Distinct().ToList();
            
            await Logger.LogInformation("Add accounts for requisitions");

            var accounts = await FireflyManager.GetAccounts();
            var requisitionFireflyAccounts = requisitionAccounts.Select(AccountMapper.GetAccount).DistinctBy(t => new {t.Name, t.Iban, t.Type}).ToList();
            var newRequisitionFireflyAccounts = requisitionFireflyAccounts.Where(a => !accounts.Contains(a)).ToList();
            await FireflyManager.AddNewAccounts(newRequisitionFireflyAccounts);
            
            await Logger.LogInformation($"Added {newRequisitionFireflyAccounts.Count} new accounts for requisitions");
            await Logger.LogInformation($"Start import for {requisitions.Count} connected banks");
            
            var firstImport = !existingFireflyTransactions.Any();
            var balances = await GetBalances(requisitions);
            var newTransactions = fromDate != DateTime.MinValue ? await GetTransactionsFromDate(requisitions, fromDate) : await GetTransactions(requisitions);

            Statistic.TotalAccounts = requisitions.Sum(r => r.Accounts.Count);

            await Logger.LogInformation($"Retrieved a total of {newTransactions.Count} transactions");

            accounts = await FireflyManager.GetAccounts();
            var tag = await CreateImportTag();
            Statistic.ImportDate = tag.Date;

            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts, tag.Tag).ToList();
            newFireflyTransactions = (await FilterTransactions(newFireflyTransactions, existingFireflyTransactions, balances.Keys)).ToList();

            if (!newFireflyTransactions.Any())
            {
                await Logger.LogInformation("No new transactions to import");
                await Logger.LogInformation("Dropped import tag");
                return;
            }

            await FireflyManager.AddNewTag(tag);

            await Logger.LogInformation($"Created the import tag: {tag.Tag}");

            newFireflyTransactions = newFireflyTransactions.Where(t => !string.Equals(t.Amount, "0")).OrderBy(t => t.Date).ToList();
            var possibleNewAccounts = newFireflyTransactions.SelectMany(t => AccountMapper.GetAccountsForTransaction(t, requisitionIbans)).DistinctBy(t => new {t.Name, t.Iban, t.Type}).ToList();
            var newAccounts = possibleNewAccounts.Where(a => !accounts.Contains(a)).ToList();

            await ImportAccounts(newAccounts);

            await ImportTransactions(newFireflyTransactions);
            Statistic.NewTransactions = newFireflyTransactions.Count;

            if (!firstImport)
                return;

            accounts = await FireflyManager.GetAccounts();
            await SetStartingBalances(balances, accounts);

            Statistic.StartingBalanceSet = true;
        }


        /// <summary>
        /// Filters the transaction list.
        /// </summary>
        /// <param name="newTransactions">The new transactions to filter.</param>
        /// <param name="existingTransactions">The existing transactions.</param>
        /// <param name="requisitionIbans">The requisition Ibans.</param>
        /// <returns></returns>
        private async Task<ICollection<FireflyTransaction>> FilterTransactions(ICollection<FireflyTransaction> newTransactions,
                                                                               ICollection<FireflyTransaction> existingTransactions,
                                                                               ICollection<string> requisitionIbans)
        {
            return await FilterTransactionsWithProcessDirector(newTransactions, existingTransactions);
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