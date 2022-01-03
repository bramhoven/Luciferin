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

        public ImportManager(INordigenManager nordigenManager, IFireflyManager fireflyManager, IImportConfiguration importConfiguration, ILogger<ImportManager> logger) : base(nordigenManager, fireflyManager, importConfiguration, logger) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override async ValueTask StartImport(CancellationToken cancellationToken)
        {
            var existingFireflyTransactions = await GetExistingFireflyTransactions();
            var requisitions = await GetRequisitions();

            Logger.LogInformation($"Start import for {requisitions.Count} connected banks");

            var firstImport = !existingFireflyTransactions.Any();
            var balances = new Dictionary<string, string>();
            var newTransactions = new List<Transaction>();
            foreach (var requisition in requisitions)
            {
                newTransactions.AddRange(await GetTransactionForRequisition(requisition));
                
                var details = await NordigenManager.GetAccountDetails(requisition.Accounts.FirstOrDefault());
                var balance = await NordigenManager.GetAccountBalance(requisition.Accounts.FirstOrDefault());
                balances.Add(details.Iban, balance.FirstOrDefault()?.BalanceAmount.Amount);
            }
            
            Logger.LogInformation($"Retrieved a total of {newTransactions.Count} transactions");

            var accounts = await FireflyManager.GetAccounts();
            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts).ToList();
            
            newFireflyTransactions = RemoveExistingTransactions(newFireflyTransactions, existingFireflyTransactions).ToList();
            newFireflyTransactions = CheckForDuplicateTransfers(newFireflyTransactions, existingFireflyTransactions, balances.Keys).ToList();

            if (!newFireflyTransactions.Any())
            {
                Logger.LogInformation("No new transactions to import");
                return;
            }

            newFireflyTransactions = newFireflyTransactions.OrderBy(t => t.Date).ToList();
            
            await ImportTransactions(newFireflyTransactions);
            
            accounts = await FireflyManager.GetAccounts();
            await SetStartingBalances(balances, accounts);
        }

        #endregion
    }
}