using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.BusinessLayer.Converters.Helper;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.BusinessLayer.Settings;

namespace Luciferin.BusinessLayer.Import.Mappers
{
    public class TransactionMapper
    {
        #region Constructors

        public TransactionMapper(ConverterHelper converterHelper, AccountMapper accountMapper, ISettingsManager settingsManager)
        {
            _converterHelper = converterHelper;
            _accountMapper = accountMapper;
            _settings = settingsManager.GetPlatformSettings();
        }

        #endregion

        #region Fields

        private readonly ConverterHelper _converterHelper;

        private readonly PlatformSettings _settings;

        private AccountMapper _accountMapper;

        #endregion

        #region Methods

        public IEnumerable<FireflyTransaction> MapTransactionsToFireflyTransactions(IEnumerable<Transaction> transactions, ICollection<FireflyAccount> fireflyAccounts,
                                                                                    string tag)
        {
            return transactions.Select(t => MapTransactionToFireflyTransaction(t, fireflyAccounts, tag)).Where(t => t != null).ToList();
        }

        /// <summary>
        /// Checks whether both Firefly accounts are of type asset.
        /// </summary>
        /// <param name="a0">Firefly account 1.</param>
        /// <param name="a1">Firefly account 2.</param>
        /// <returns></returns>
        private bool CheckAllAssetAccounts(FireflyAccount a0, FireflyAccount a1)
        {
            return CheckAssetAccount(a0) && CheckAssetAccount(a1);
        }

        /// <summary>
        /// Checks whether the account is an asset account.
        /// </summary>
        /// <param name="a">Firefly account.</param>
        /// <returns></returns>
        private bool CheckAssetAccount(FireflyAccount a)
        {
            return a.Type == AccountType.Asset;
        }

        /// <summary>
        /// Checks whether the transfer originates from the destination bank.
        /// </summary>
        /// <param name="fireflyTransaction">The mapped Firefly transaction.</param>
        /// <param name="originalTransaction">The original imported transaction.</param>
        /// <returns></returns>
        private bool CheckTransferOriginatesFromDestination(FireflyTransaction fireflyTransaction, Transaction originalTransaction)
        {
            if (fireflyTransaction.Type != TransactionType.Transfer)
                return false;

            return string.Equals(originalTransaction.RequisitorIban, fireflyTransaction.DestinationIban, StringComparison.InvariantCultureIgnoreCase);
        }

        private FireflyTransaction MapTransactionToFireflyTransaction(Transaction transaction, ICollection<FireflyAccount> fireflyAccounts, string tag)
        {
            if (_settings.FilterAuthorisations.Value &&
                string.Equals(transaction.BankTransactionCode, BankTransactionCodes.Authorisation, StringComparison.InvariantCultureIgnoreCase))
                return null;

            var converter = _converterHelper.GetTransactionConverter(transaction.RequisitorBank);
            var fireflyTransaction = converter.ConvertTransaction(transaction, tag);

            FireflyAccount source;
            FireflyAccount destination;

            if (!string.IsNullOrWhiteSpace(transaction.CreditorName))
            {
                source = _accountMapper.GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset });
                destination = _accountMapper.GetAccount(fireflyAccounts, transaction.CreditorName, transaction.CreditorAccount?.Iban,
                                         new[] { AccountType.Asset, AccountType.Expense });
                fireflyTransaction.Type = CheckAllAssetAccounts(source, destination) ? TransactionType.Transfer : TransactionType.Withdrawal;
            }
            else if (!string.IsNullOrWhiteSpace(transaction.DebtorName))
            {
                source = _accountMapper.GetAccount(fireflyAccounts, transaction.DebtorName, transaction.DebtorAccount?.Iban, new[] { AccountType.Asset, AccountType.Revenue });
                destination = _accountMapper.GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset });
                fireflyTransaction.Type = CheckAllAssetAccounts(source, destination) ? TransactionType.Transfer : TransactionType.Deposit;
            }
            else if (transaction.TransactionAmount.Amount.Contains("-"))
            {
                source = _accountMapper.GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset });
                destination = _accountMapper.GetAccount(fireflyAccounts, fireflyTransaction.Description, string.Empty, new[] { AccountType.Asset, AccountType.Expense });
                fireflyTransaction.Type = CheckAllAssetAccounts(source, destination) ? TransactionType.Transfer : TransactionType.Withdrawal;
            }
            else
            {
                source = _accountMapper.GetAccount(fireflyAccounts, fireflyTransaction.Description, string.Empty, new[] { AccountType.Revenue });
                destination = _accountMapper.GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset, AccountType.Expense });
                fireflyTransaction.Type = CheckAllAssetAccounts(source, destination) ? TransactionType.Transfer : TransactionType.Deposit;
            }

            fireflyTransaction.SourceId = source?.Id ?? 0;
            fireflyTransaction.SourceIban = source.Iban;
            fireflyTransaction.SourceName = source.Name;
            fireflyTransaction.SourceType = source.Type.ToString();
            fireflyTransaction.DestinationId = destination?.Id ?? 0;
            fireflyTransaction.DestinationIban = destination.Iban;
            fireflyTransaction.DestinationName = destination.Name;
            fireflyTransaction.DestinationType = destination.Type.ToString();

            if (CheckTransferOriginatesFromDestination(fireflyTransaction, transaction))
                return null;

            return fireflyTransaction;
        }

        #endregion
    }
}