using System;
using System.Collections.Generic;
using System.Linq;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Import.Mappers
{
    public static class TransactionMapper
    {
        #region Methods

        #region Static Methods

        public static IEnumerable<FireflyTransaction> MapTransactionsToFireflyTransactions(IEnumerable<Transaction> transactions, ICollection<FireflyAccount> fireflyAccounts)
        {
            return transactions.Select(t => MapTransactionToFireflyTransaction(t, fireflyAccounts)).Where(t => t != null).ToList();
        }

        public static FireflyTransaction MapTransactionToFireflyTransaction(Transaction transaction, ICollection<FireflyAccount> fireflyAccounts)
        {
            var fireflyTransaction = new FireflyTransaction
            {
                Amount = transaction.TransactionAmount.Amount.Replace("-", ""),
                Date = transaction.BookingDate,
                Description = transaction.CreditorName ?? transaction.DebtorName,
                ExternalId = transaction.TransactionId,
            };

            FireflyAccount source;
            FireflyAccount destination;

            if (!string.IsNullOrWhiteSpace(transaction.CreditorName))
            {
                source = GetAccount(fireflyAccounts, transaction.RequisitorIban, new [] {AccountType.Asset});
                destination = GetAccount(fireflyAccounts, transaction.CreditorName, transaction.CreditorAccount?.Iban, new [] {AccountType.Asset, AccountType.Expense});
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Withdrawal;
            }
            else if (!string.IsNullOrWhiteSpace(transaction.DebtorName))
            {
                source = GetAccount(fireflyAccounts, transaction.DebtorName, transaction.DebtorAccount?.Iban, new [] {AccountType.Asset, AccountType.Revenue});
                destination = GetAccount(fireflyAccounts, transaction.RequisitorIban, new [] {AccountType.Asset});
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Deposit;
            }
            else if(transaction.TransactionAmount.Amount.Contains("-"))
            {
                source = GetAccount(fireflyAccounts, transaction.RequisitorIban, new [] {AccountType.Asset});
                destination = GetAccount(fireflyAccounts, transaction.RemittanceInformationUnstructured, string.Empty, new [] {AccountType.Asset, AccountType.Expense});
                fireflyTransaction.Description = transaction.RemittanceInformationUnstructured;
                fireflyTransaction.Type = source.Type == AccountType.Asset && destination.Type == AccountType.Asset ? TransactionType.Transfer : TransactionType.Withdrawal;
            }
            else
            {
                source = GetAccount(fireflyAccounts, "Bank", string.Empty, new [] {AccountType.Revenue});
                destination = GetAccount(fireflyAccounts, transaction.RequisitorIban, new [] {AccountType.Asset, AccountType.Expense});
                fireflyTransaction.Description = "Deposit";
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

        private static FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string iban, AccountType[] accountTypes)
        {
            return accounts.FirstOrDefault(a => string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) && accountTypes.Contains(a.Type));
        }

        private static FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string name, string iban, AccountType[] accountTypes)
        {
            var account = accounts.FirstOrDefault(a => (string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) || string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase)) && accountTypes.Contains(a.Type)) ?? new FireflyAccount
            {
                Iban = iban,
                Name = name
            };

            return account;
        }

        #endregion

        #endregion
    }
}