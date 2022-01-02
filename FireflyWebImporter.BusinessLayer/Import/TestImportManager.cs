using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Configuration.Interfaces;
using FireflyWebImporter.BusinessLayer.Firefly;
using FireflyWebImporter.BusinessLayer.Import.Mappers;
using FireflyWebImporter.BusinessLayer.Nordigen;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.Logging;

namespace FireflyWebImporter.BusinessLayer.Import
{
    public class TestImportManager : ImportManagerBase
    {
        #region Constructors

        public TestImportManager(INordigenManager nordigenManager, IFireflyManager fireflyManager, ICompareConfiguration compareConfiguration, ILogger<ImportManager> logger) : base(nordigenManager, fireflyManager, compareConfiguration, logger) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override async ValueTask StartImport(CancellationToken cancellationToken)
        {
            var existingFireflyTransactions = await GetExistingFireflyTransactions();
            var requisitions = await GetRequisitions();

            Logger.LogInformation($"Start test import for {requisitions.Count} connected banks");

            var newTransactions = new List<Transaction>();
            foreach (var requisition in requisitions)
                newTransactions.AddRange(await GetTransactionForRequisition(requisition));

            var accounts = await FireflyManager.GetAccounts();
            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts).ToList();
            
            newFireflyTransactions = RemoveExistingTransactions(newFireflyTransactions, existingFireflyTransactions).ToList();
            newFireflyTransactions = CheckForDuplicateTransactions(newFireflyTransactions, existingFireflyTransactions).ToList();

            if (!newFireflyTransactions.Any())
            {
                Logger.LogInformation("No new transactions to import");
                return;
            }

            Logger.LogInformation($"Would import {newFireflyTransactions.Count} transactions");
        }

        #endregion
    }
}