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
    public class ImportManager : ImportManagerBase
    {
        #region Constructors

        public ImportManager(INordigenManager nordigenManager, IFireflyManager fireflyManager, ICompareConfiguration compareConfiguration, ILogger<ImportManager> logger) : base(nordigenManager, fireflyManager, compareConfiguration, logger) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override async ValueTask StartImport(CancellationToken cancellationToken)
        {
            var existingFireflyTransactions = await GetExistingFireflyTransactions();
            var requisitions = await GetRequisitions();

            Logger.LogInformation($"Start import for {requisitions.Count} connected banks");

            var newTransactions = new List<Transaction>();
            foreach (var requisition in requisitions)
                newTransactions.AddRange(await GetTransactionForRequisition(requisition));
            
            Logger.LogInformation($"Retrieved a total of {newTransactions.Count} transactions");

            var accounts = await FireflyManager.GetAccounts();
            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts).ToList();
            
            newFireflyTransactions = RemoveExistingTransactions(newFireflyTransactions, existingFireflyTransactions).ToList();
            newFireflyTransactions = CheckForDuplicateTransactions(newFireflyTransactions, existingFireflyTransactions).ToList();

            if (!newFireflyTransactions.Any())
            {
                Logger.LogInformation("No new transactions to import");
                return;
            }

            newFireflyTransactions = newFireflyTransactions.OrderBy(t => t.Date).ToList();
            await ImportTransactions(newFireflyTransactions);
        }

        #endregion
    }
}