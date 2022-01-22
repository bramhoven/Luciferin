﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Configuration.Interfaces;
using Luciferin.BusinessLayer.Firefly;
using Luciferin.BusinessLayer.Import.Mappers;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.BusinessLayer.Nordigen.Models;
using Microsoft.Extensions.Logging;

namespace Luciferin.BusinessLayer.Import
{
    public class ImportManager : ImportManagerBase
    {
        #region Constructors

        public ImportManager(INordigenManager nordigenManager,
                             IFireflyManager fireflyManager,
                             IImportConfiguration importConfiguration,
                             ICompositeLogger<ImportManager> logger) : base(nordigenManager, fireflyManager, importConfiguration, logger) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override async ValueTask StartImport(CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);
            await Logger.LogInformation("Starting import");
            
            var existingFireflyTransactions = await GetExistingFireflyTransactions();
            var requisitions = await GetRequisitions();

            await Logger.LogInformation($"Start import for {requisitions.Count} connected banks");

            var firstImport = !existingFireflyTransactions.Any();
            var balances = new Dictionary<string, string>();
            var newTransactions = new List<Transaction>();
            foreach (var requisition in requisitions)
            {
                foreach (var account in requisition.Accounts)
                {
                    newTransactions.AddRange(await GetTransactionForRequisitionAccount(account, requisition));

                    var details = await NordigenManager.GetAccountDetails(account);
                    var balance = await NordigenManager.GetAccountBalance(account);
                    balances.Add(details.Iban, balance.FirstOrDefault()?.BalanceAmount.Amount);
                }
            }

            await Logger.LogInformation($"Retrieved a total of {newTransactions.Count} transactions");

            var accounts = await FireflyManager.GetAccounts();
            var tag = await CreateImportTag();
            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts, tag.Tag).ToList();

            newFireflyTransactions = (await RemoveExistingTransactions(newFireflyTransactions, existingFireflyTransactions)).ToList();
            newFireflyTransactions = (await CheckForDuplicateTransfers(newFireflyTransactions, existingFireflyTransactions, balances.Keys)).ToList();

            if (!newFireflyTransactions.Any())
            {
                await Logger.LogInformation("No new transactions to import");
                return;
            }

            newFireflyTransactions = newFireflyTransactions.OrderBy(t => t.Date).ToList();
            await ImportTransactions(newFireflyTransactions);

            if (!firstImport)
                return;

            accounts = await FireflyManager.GetAccounts();
            await SetStartingBalances(balances, accounts);
        }

        #endregion
    }
}