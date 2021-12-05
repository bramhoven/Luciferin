using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Import.Mappers;
using FireflyWebImporter.BusinessLayer.Nordigen;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.Logging;

namespace FireflyWebImporter.BusinessLayer.Import
{
    public class ImportManager : IImportManager
    {
        #region Fields

        private readonly IFireflyManager _fireflyManager;

        private readonly INordigenManager _nordigenManager;

        private readonly ILogger<ImportManager> _logger;

        #endregion

        #region Constructors

        public ImportManager(INordigenManager nordigenManager, IFireflyManager fireflyManager, ILogger<ImportManager> logger)
        {
            _nordigenManager = nordigenManager;
            _fireflyManager = fireflyManager;
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<Requisition> AddNewBank(string institutionId, string bankName, string redirectUrl)
        {
            var institution = await _nordigenManager.GetInstitution(institutionId);
            var endUserAgreement = await _nordigenManager.CreateEndUserAgreement(institution);
            return await _nordigenManager.CreateRequisition(institution, bankName, endUserAgreement, redirectUrl);
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

        /// <param name="cancellationToken"></param>
        /// <inheritdoc />
        public async ValueTask StartImport(CancellationToken cancellationToken)
        {
            var fireflyTransactions = await GetTransactions();
            var requisitions = await GetRequisitions();

            _logger.LogInformation($"Start import for {requisitions.Count} connected banks");

            var newTransactions = new List<Transaction>();
            foreach (var requisition in requisitions)
            {
                var account = requisition.Accounts.FirstOrDefault();

                var transactions = await _nordigenManager.GetAccountTransactions(account);
                var details = await _nordigenManager.GetAccountDetails(account);

                foreach (var transaction in transactions)
                    transaction.RequisitorIban = details.Iban;

                var newAccountTransactions = CompareTransactions(transactions, fireflyTransactions);
                newTransactions.AddRange(newAccountTransactions);

                _logger.LogInformation($"{details.Iban} has {newAccountTransactions.Count}/{transactions.Count} new transactions");
            }

            var accounts = await _fireflyManager.GetAccounts();
            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts).ToList();

            if (!newFireflyTransactions.Any())
            {
                _logger.LogInformation("No new transactions to import");
                return;
            }

            await _fireflyManager.AddNewTransactions(newFireflyTransactions);

            _logger.LogInformation($"Imported {newFireflyTransactions.Count} transactions");
        }

        private async Task<ICollection<FireflyTransaction>> GetTransactions()
        {
            return await _fireflyManager.GetTransactions();
        }

        #region Static Methods

        private static ICollection<Transaction> CompareTransactions(IEnumerable<Transaction> transactions, ICollection<FireflyTransaction> fireflyTransactions)
        {
            return transactions.Where(t => fireflyTransactions.All(ft => string.Equals(ft.ExternalId, t.TransactionId, StringComparison.InvariantCultureIgnoreCase))).ToList();
        }

        #endregion

        #endregion
    }
}