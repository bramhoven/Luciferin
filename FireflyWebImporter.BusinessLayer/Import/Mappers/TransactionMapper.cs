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
                source = GetAccount(fireflyAccounts, transaction.RequisitorIban);
                destination = GetAccount(fireflyAccounts, transaction.CreditorName, transaction.CreditorAccount?.Iban);
                fireflyTransaction.Type = TransactionType.Withdrawal;
            }
            else if (!string.IsNullOrWhiteSpace(transaction.DebtorName))
            {
                source = GetAccount(fireflyAccounts, transaction.DebtorName, transaction.DebtorAccount?.Iban);
                destination = GetAccount(fireflyAccounts, transaction.RequisitorIban);
                fireflyTransaction.Type = TransactionType.Deposit;
            }
            else if(transaction.TransactionAmount.Amount.Contains("-"))
            {
                source = GetAccount(fireflyAccounts, transaction.RequisitorIban);
                destination = GetAccount(fireflyAccounts, "Bank", string.Empty);
                fireflyTransaction.Description = "Banking fee";
                fireflyTransaction.Type = TransactionType.Withdrawal;
            }
            else
            {
                source = GetAccount(fireflyAccounts, "Bank", string.Empty);
                destination = GetAccount(fireflyAccounts, transaction.RequisitorIban);
                fireflyTransaction.Description = "Deposit";
                fireflyTransaction.Type = TransactionType.Deposit;
            }

            fireflyTransaction.SourceId = source?.Id ?? 0;
            fireflyTransaction.SourceIban = source.Iban;
            fireflyTransaction.SourceName = source.Name;
            fireflyTransaction.DestinationId = destination?.Id ?? 0;
            fireflyTransaction.DestinationIban = destination.Iban;
            fireflyTransaction.DestinationName = destination.Name;

            return fireflyTransaction;
        }

        private static FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string iban)
        {
            return accounts.FirstOrDefault(a => string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase));
        }

        private static FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string name, string iban)
        {
            var account = accounts.FirstOrDefault(a => string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) || string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase)) ?? new FireflyAccount
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