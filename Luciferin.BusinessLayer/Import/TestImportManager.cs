﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly;
using Luciferin.BusinessLayer.Import.Mappers;
using Luciferin.BusinessLayer.Import.Stores;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Settings;

namespace Luciferin.BusinessLayer.Import
{
    public class TestImportManager : ImportManagerBase
    {
        #region Constructors

        public TestImportManager(INordigenManager nordigenManager,
                                 IFireflyManager fireflyManager,
                                 ISettingsManager settingsManager,
                                 IImportStatisticsStore importStatisticsStore,
                                 TransactionMapper transactionMapper,
                                 AccountMapper accountMapper,
                                 ICompositeLogger<TestImportManager> logger) : base(nordigenManager, fireflyManager, settingsManager, importStatisticsStore, transactionMapper, accountMapper, logger) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override async ValueTask RunImport(CancellationToken cancellationToken)
        {
            var fromDate = DateTime.Today.AddDays(-PlatformSettings.ImportDays.Value);
            var existingFireflyTransactions = await GetExistingFireflyTransactions(fromDate);
            var requisitions = await GetImportableRequisitions();

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
            var newFireflyTransactions = TransactionMapper.MapTransactionsToFireflyTransactions(newTransactions, accounts, null).ToList();

            newFireflyTransactions = (await RemoveExistingTransactions(newFireflyTransactions, existingFireflyTransactions)).ToList();
            newFireflyTransactions = (await CheckForDuplicateTransfers(newFireflyTransactions, existingFireflyTransactions, balances.Keys)).ToList();

            if (!newFireflyTransactions.Any())
            {
                await Logger.LogInformation("No new transactions to import");
                return;
            }

            await Logger.LogInformation($"Would import {newFireflyTransactions.Count} transactions");

            if (!firstImport)
                return;

            await Logger.LogInformation("Would set asset account opening balances");
        }

        /// <inheritdoc />
        protected override ValueTask RunImport(DateTime fromDate, CancellationToken cancellationToken)
        {
            _ = fromDate;
            
            return RunImport(cancellationToken);
        }

        #endregion
    }
}