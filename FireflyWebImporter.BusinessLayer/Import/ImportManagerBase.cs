using System;
using System.Collections.Generic;
using FireflyWebImporter.BusinessLayer.Helpers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Configuration.Interfaces;
using FireflyWebImporter.BusinessLayer.Converters.Helper;
using FireflyWebImporter.BusinessLayer.Firefly;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.Logging;

namespace FireflyWebImporter.BusinessLayer.Import
{
    public abstract class ImportManagerBase : IImportManager
    {
        #region Fields

        private readonly ICompareConfiguration _compareConfiguration;

        private readonly INordigenManager _nordigenManager;

        protected readonly IFireflyManager FireflyManager;

        protected readonly ILogger<IImportManager> Logger;

        #endregion

        #region Constructors

        protected ImportManagerBase(INordigenManager nordigenManager, IFireflyManager fireflyManager, ICompareConfiguration compareConfiguration, ILogger<IImportManager> logger)
        {
            _nordigenManager = nordigenManager;
            FireflyManager = fireflyManager;
            _compareConfiguration = compareConfiguration;
            Logger = logger;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<Requisition> AddNewBank(string institutionId, string name, string redirectUrl)
        {
            var institution = await _nordigenManager.GetInstitution(institutionId);
            var endUserAgreement = await _nordigenManager.CreateEndUserAgreement(institution);
            return await _nordigenManager.CreateRequisition(institution, name, endUserAgreement, redirectUrl);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteBank(string requisitionId)
        {
            try
            {
                var requisition = await _nordigenManager.GetRequisition(requisitionId);
                var endUserAgreement = await _nordigenManager.GetEndUserAgreement(requisition.Agreement);

                await _nordigenManager.DeleteEndUserAgreement(endUserAgreement.Id);
                await _nordigenManager.DeleteRequisition(requisition.Id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public Task<ICollection<Requisition>> GetRequisitions()
        {
            return _nordigenManager.GetRequisitions();
        }

        /// <inheritdoc />
        public abstract ValueTask StartImport(CancellationToken cancellationToken);

        /// <summary>
        /// Checks for duplicate transactions.
        /// For example:
        /// A transfer from one asset account to another.
        /// </summary>
        /// <param name="transactions">The new transactions.</param>
        /// <param name="fireflyTransactions">The existing firefly transactions.</param>
        /// <returns></returns>
        protected IEnumerable<FireflyTransaction> CheckForDuplicateTransactions(IEnumerable<FireflyTransaction> transactions, ICollection<FireflyTransaction> fireflyTransactions)
        {
            Logger.LogInformation("Checking transactions for duplicates");
            
            var nonDuplicateTransactions = new List<FireflyTransaction>();

            var combinedTransactions = transactions.Concat(fireflyTransactions);
            var groupedByType = combinedTransactions.GroupBy(t => t.Type);
            foreach (var typedTransactionGroup in groupedByType)
            {
                var possibleDuplicates = typedTransactionGroup.GroupBy(t => (t.SourceIban, t.DestinationIban, t.Amount).GetConsistentHash());
                foreach (var duplicateGroup in possibleDuplicates)
                {
                    var duplicates = duplicateGroup.ToList();
                    if (duplicates.Count == 1)
                    {
                        nonDuplicateTransactions.Add(duplicates.FirstOrDefault());
                        continue;
                    }

                    var orderedDuplicateGroup = duplicates.OrderBy(t => t.Date).ToList();
                    var first = orderedDuplicateGroup.FirstOrDefault();
                    if (first == null)
                        continue;

                    var maxDate = first.Date.AddDays(_compareConfiguration.DuplicateTransactionDayRange);
                    var nonDuplicates = orderedDuplicateGroup.Skip(1).Where(t => t.Date < first.Date && t.Date > maxDate).ToList();
                    if (nonDuplicates.Any())
                    {
                        nonDuplicateTransactions.AddRange(nonDuplicates);
                        foreach (var nonDuplicate in nonDuplicates)
                            duplicates.Remove(nonDuplicate);
                    }

                    if(duplicates.Any(d => !string.IsNullOrWhiteSpace(d.Id)))
                        continue;

                    if (fireflyTransactions.Contains(first))
                        continue;
                    
                    nonDuplicateTransactions.Add(first);

                    Logger.LogInformation($"Detected duplicate for {first.Description} ({first.Amount:F2}) [{first.SourceIban} -> {first.DestinationIban}]. Inserting first one.");
                }
            }

            nonDuplicateTransactions = nonDuplicateTransactions.Where(t => transactions.Contains(t) && !fireflyTransactions.Contains(t)).ToList();
            
            Logger.LogInformation($"{nonDuplicateTransactions.Count} transactions left after duplicate check");

            return nonDuplicateTransactions;
        }

        /// <summary>
        /// Gets existing Firefly transactions.
        /// </summary>
        /// <returns></returns>
        protected async Task<ICollection<FireflyTransaction>> GetExistingFireflyTransactions()
        {
            Logger.LogInformation("Getting existing Firefly transactions");

            var transactions = await FireflyManager.GetTransactions();

            Logger.LogInformation($"Retrieved {transactions.Count} existing Firefly transactions");

            return transactions;
        }

        /// <summary>
        /// Gets all the transactions for a requisition.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        protected async Task<ICollection<Transaction>> GetTransactionForRequisition(Requisition requisition)
        {
            var account = requisition.Accounts.FirstOrDefault();
            var details = await _nordigenManager.GetAccountDetails(account);

            Logger.LogInformation($"Getting all transactions for {details.Iban}");

            var transactions = await _nordigenManager.GetAccountTransactions(account, DateTime.Today.AddDays(-10));

            foreach (var transaction in transactions)
                ExtendData(transaction, requisition, details);

            Logger.LogInformation($"Retrieved {transactions.Count} transactions for {details.Iban}");

            return transactions;
        }

        /// <summary>
        /// Imports a list of firefly transactions.
        /// </summary>
        /// <param name="fireflyTransactions">The list of transactions to import.</param>
        /// <returns></returns>
        protected async Task ImportTransactions(ICollection<FireflyTransaction> fireflyTransactions)
        {
            Logger.LogInformation($"Start importing {fireflyTransactions.Count} transactions");

            try
            {
                await FireflyManager.AddNewTransactions(fireflyTransactions);
                Logger.LogInformation($"Imported {fireflyTransactions.Count} transactions");
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e, e.Message);
            }
        }

        #region Static Methods

        /// <summary>
        /// Removes the existing transactions from the new transactions list.
        /// </summary>
        /// <param name="newTransactions">The new transactions.</param>
        /// <param name="existingTransactions">The existing transactions.</param>
        /// <returns></returns>
        protected IEnumerable<FireflyTransaction> RemoveExistingTransactions(IEnumerable<FireflyTransaction> newTransactions, ICollection<FireflyTransaction> existingTransactions)
        {
            Logger.LogInformation("Checking existing Firefly transactions");
            
            var transactions = newTransactions.Where(t => existingTransactions.All(ft => !string.Equals(ft.ExternalId, t.ExternalId, StringComparison.InvariantCultureIgnoreCase))).ToList();
            
            Logger.LogInformation($"{transactions.Count} transactions left after existing check");

            return transactions;
        }

        /// <summary>
        /// Extends the transaction data with requisition data.
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
}