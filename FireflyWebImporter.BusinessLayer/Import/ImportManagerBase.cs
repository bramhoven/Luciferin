using System;
using System.Collections.Generic;
using FireflyWebImporter.BusinessLayer.Helpers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Configuration.Interfaces;
using FireflyWebImporter.BusinessLayer.Converters.Helper;
using FireflyWebImporter.BusinessLayer.Firefly;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.Logging;

namespace FireflyWebImporter.BusinessLayer.Import
{
    public abstract class ImportManagerBase : IImportManager
    {
        #region Fields

        private readonly IImportConfiguration _importConfiguration;

        protected readonly INordigenManager NordigenManager;

        protected readonly IFireflyManager FireflyManager;

        protected readonly ILogger<IImportManager> Logger;

        #endregion

        #region Constructors

        protected ImportManagerBase(INordigenManager nordigenManager, IFireflyManager fireflyManager, IImportConfiguration importConfiguration, ILogger<IImportManager> logger)
        {
            NordigenManager = nordigenManager;
            FireflyManager = fireflyManager;
            _importConfiguration = importConfiguration;
            Logger = logger;
        }

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
        public Task<ICollection<Requisition>> GetRequisitions()
        {
            return NordigenManager.GetRequisitions();
        }

        /// <inheritdoc />
        public abstract ValueTask StartImport(CancellationToken cancellationToken);

        /// <summary>
        /// Checks for duplicate transfers.
        /// </summary>
        /// <param name="transactions">The new transactions.</param>
        /// <param name="fireflyTransactions">The existing firefly transactions.</param>
        /// <param name="requisitionIbans">The list of requisition ibans.</param>
        /// <returns></returns>
        protected IEnumerable<FireflyTransaction> CheckForDuplicateTransfers(IEnumerable<FireflyTransaction> transactions, ICollection<FireflyTransaction> fireflyTransactions, ICollection<string> requisitionIbans)
        {
            Logger.LogInformation("Checking transactions for duplicates transfers");

            var nonDuplicateTransactions = transactions.Where(t => t.Type != TransactionType.Transfer).ToList();

            var combinedTransactions = transactions.Concat(fireflyTransactions);
            combinedTransactions = combinedTransactions.DistinctBy(t => t.ExternalId);
            var groups = combinedTransactions
                         .Where(t => t.Type == TransactionType.Transfer)
                         .GroupBy(t => (t.SourceIban, t.DestinationIban, t.Amount).GetConsistentHash());
            foreach (var group in groups)
            {
                var possibleDuplicates = group.ToList();
                if (possibleDuplicates.Count == 1)
                {
                    nonDuplicateTransactions.Add(possibleDuplicates.FirstOrDefault());
                    continue;
                }

                var nonDuplicates = group.Where(t => requisitionIbans.Contains(t.SourceIban) && t.SourceIban.Equals(t.RequisitionIban, StringComparison.InvariantCultureIgnoreCase)
                                                     || !requisitionIbans.Contains(t.SourceIban) &&  t.DestinationIban.Equals(t.RequisitionIban, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if(nonDuplicates.Any())
                    nonDuplicateTransactions.AddRange(nonDuplicates);
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
            var details = await NordigenManager.GetAccountDetails(account);

            Logger.LogInformation($"Getting all transactions for {details.Iban}");

            ICollection<Transaction> transactions;
            if (_importConfiguration.DaysToSync > 0)
                transactions = await NordigenManager.GetAccountTransactions(account, DateTime.Today.AddDays(-_importConfiguration.DaysToSync));
            else
                transactions = await NordigenManager.GetAccountTransactions(account);

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

        #region Static Methods

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