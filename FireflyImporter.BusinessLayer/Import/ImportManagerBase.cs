﻿using System;
using System.Collections.Generic;
using System.Globalization;
using FireflyImporter.BusinessLayer.Helpers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Configuration.Interfaces;
using FireflyImporter.BusinessLayer.Converters.Helper;
using FireflyImporter.BusinessLayer.Firefly;
using FireflyImporter.BusinessLayer.Firefly.Enums;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen;
using FireflyImporter.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.Logging;

namespace FireflyImporter.BusinessLayer.Import
{
    public abstract class ImportManagerBase : IImportManager
    {
        #region Fields

        private readonly IImportConfiguration _importConfiguration;

        protected readonly IFireflyManager FireflyManager;

        protected readonly ILogger<IImportManager> Logger;

        protected readonly INordigenManager NordigenManager;

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
        public async Task<ICollection<Requisition>> GetRequisitions()
        {
            var requisitions = await NordigenManager.GetRequisitions();

            foreach (var requisition in requisitions)
            {
                var removableAccounts = new List<string>();
                foreach (var currentAccount in requisition.Accounts)
                {
                    var details = await NordigenManager.GetAccountDetails(currentAccount);
                    if (string.IsNullOrWhiteSpace(details.Iban))
                        removableAccounts.Add(currentAccount);
                }

                foreach (var removableAccount in removableAccounts)
                    requisition.Accounts.Remove(removableAccount);
            }

            return requisitions;
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
                                                     || !requisitionIbans.Contains(t.SourceIban) && t.DestinationIban.Equals(t.RequisitionIban, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (nonDuplicates.Any())
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
        /// <param name="accountId">The account id for which to get the transactions.</param>
        /// <param name="requisition">The requisition to extend data.</param>
        /// <returns></returns>
        protected async Task<ICollection<Transaction>> GetTransactionForRequisitionAccount(string accountId, Requisition requisition)
        {
            var details = await NordigenManager.GetAccountDetails(accountId);

            Logger.LogInformation($"Getting all transactions for {details.Iban}");

            ICollection<Transaction> transactions;
            if (_importConfiguration.DaysToSync > 0)
                transactions = await NordigenManager.GetAccountTransactions(accountId, DateTime.Today.AddDays(-_importConfiguration.DaysToSync));
            else
                transactions = await NordigenManager.GetAccountTransactions(accountId);

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
                var dateString = DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy");
                var tag = $"Imported by Firefly III Importer | {dateString}";

                foreach (var transaction in fireflyTransactions)
                {
                    if (transaction.Tags == null)
                        transaction.Tags = new List<string>();

                    transaction.Tags.Add(tag);
                }

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

        /// <summary>
        /// Sets the correct starting balances.
        /// </summary>
        /// <param name="currentBalances">The dictionary with iban and current balance.</param>
        /// <param name="fireflyAccounts">The list of all Firefly accounts.</param>
        protected async Task SetStartingBalances(IDictionary<string, string> currentBalances, ICollection<FireflyAccount> fireflyAccounts)
        {
            Logger.LogInformation("First import detected");
            Logger.LogInformation("Start setting starting balances");

            foreach (var account in currentBalances)
            {
                var iban = account.Key;
                var bankBalance = decimal.Parse(account.Value, new CultureInfo("en-US"));

                var fireflyAccount = fireflyAccounts.FirstOrDefault(a => string.Equals(a.Iban, iban, StringComparison.InvariantCultureIgnoreCase));
                if (fireflyAccount == null)
                    continue;

                var transaction = await FireflyManager.GetFirstTransactionOfAccount(fireflyAccount.Id);

                var currentFireflyBalance = decimal.Parse(fireflyAccount.CurrentBalance, new CultureInfo("en-US"));
                var openingBalance = bankBalance - currentFireflyBalance;

                fireflyAccount.OpeningBalanceDate = transaction.Date.Date.AddDays(-1);
                fireflyAccount.OpeningBalance = openingBalance.ToString("0.00", CultureInfo.InvariantCulture);

                await FireflyManager.UpdateAccount(fireflyAccount);

                Logger.LogInformation($"[{fireflyAccount.Name}] Set opening balance to: {openingBalance.ToString("0.00", CultureInfo.InvariantCulture)}");
            }

            Logger.LogInformation("Finished setting starting balances");
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