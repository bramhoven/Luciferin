using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.BusinessLayer.Converters.Helper;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Import.Mappers
{
    public class TransactionMapper
    {
        #region Fields

        private readonly ConverterHelper _converterHelper;

        #endregion

        #region Constructors

        public TransactionMapper(ConverterHelper converterHelper)
        {
            _converterHelper = converterHelper;
        }

        #endregion

        #region Methods

        public IEnumerable<FireflyTransaction> MapTransactionsToFireflyTransactions(IEnumerable<Transaction> transactions, ICollection<FireflyAccount> fireflyAccounts, string tag)
        {
            return transactions.Select(t => MapTransactionToFireflyTransaction(t, fireflyAccounts, tag)).Where(t => t != null).ToList();
        }

        private FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string iban, AccountType[] accountTypes)
        {
            return accounts.FirstOrDefault(a => string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) && accountTypes.Contains(a.Type));
        }

        private FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string name, string iban, AccountType[] accountTypes)
        {
            var account = accounts.FirstOrDefault(a => (!string.IsNullOrWhiteSpace(iban) && string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) || string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase)) && accountTypes.Contains(a.Type)) ?? new FireflyAccount
            {
                Iban = iban,
                Name = name
            };

            return account;
        }

        private FireflyTransaction MapTransactionToFireflyTransaction(Transaction transaction, ICollection<FireflyAccount> fireflyAccounts, string tag)
        {
            var converter = _converterHelper.GetTransactionConverter(transaction.RequisitorBank);
            var fireflyTransaction = converter.ConvertTransaction(transaction, tag);

            FireflyAccount source;
            FireflyAccount destination;

            if (!string.IsNullOrWhiteSpace(transaction.CreditorName))
            {
                source = GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset });
                destination = GetAccount(fireflyAccounts, transaction.CreditorName, transaction.CreditorAccount?.Iban, new[] { AccountType.Asset, AccountType.Expense });
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Withdrawal;
            }
            else if (!string.IsNullOrWhiteSpace(transaction.DebtorName))
            {
                source = GetAccount(fireflyAccounts, transaction.DebtorName, transaction.DebtorAccount?.Iban, new[] { AccountType.Asset, AccountType.Revenue });
                destination = GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset });
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Deposit;
            }
            else if (transaction.TransactionAmount.Amount.Contains("-"))
            {
                source = GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset });
                destination = GetAccount(fireflyAccounts, fireflyTransaction.Description, string.Empty, new[] { AccountType.Asset, AccountType.Expense });
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Withdrawal;
            }
            else
            {
                source = GetAccount(fireflyAccounts, fireflyTransaction.Description, string.Empty, new[] { AccountType.Revenue });
                destination = GetAccount(fireflyAccounts, transaction.RequisitorIban, new[] { AccountType.Asset, AccountType.Expense });
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Deposit;
            }

            fireflyTransaction.SourceId = source?.Id ?? 0;
            fireflyTransaction.SourceIban = source.Iban;
            fireflyTransaction.SourceName = source.Name;
            fireflyTransaction.DestinationId = destination?.Id ?? 0;
            fireflyTransaction.DestinationIban = destination.Iban;
            fireflyTransaction.DestinationName = destination.Name;

            return fireflyTransaction;
        }

        #endregion
    }
}