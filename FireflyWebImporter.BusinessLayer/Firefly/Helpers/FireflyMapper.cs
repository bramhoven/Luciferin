using System;
using System.Collections.Generic;
using System.Linq;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Responses;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Responses.Accounts;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Responses.Transactions;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Shared;

namespace FireflyWebImporter.BusinessLayer.Firefly.Helpers
{
    public static class FireflyMapper
    {
        #region Methods

        #region Static Methods

        private static FireflyAccount MapToFireflyAccount(this FireflyDataContainer<FireflyAccountAttributes> fireflyDataContainer)
        {
            var account = fireflyDataContainer.Attributes;
            if (account == null)
                throw new ArgumentNullException(nameof(fireflyDataContainer.Attributes));

            return new FireflyAccount
            {
                Active = account.Active,
                AccountNumber = account.AccountNumber,
                AccountRole = account.AccountRole,
                Bic = account.Bic,
                CurrencyCode = account.CurrencyCode,
                CurrencyId = account.CurrencyId,
                CurrencySymbol = account.CurrencySymbol,
                CurrentBalance = account.CurrentBalance,
                CurrentDebt = account.CurrentDebt,
                CurrencyDecimalPlaces = account.CurrencyDecimalPlaces,
                CurrentBalanceDate = account.CurrentBalanceDate,
                CreditCardType = account.CreditCardType,
                Id = int.Parse(fireflyDataContainer.Id),
                Iban = account.Iban,
                Interest = account.Interest,
                InterestPeriod = account.InterestPeriod,
                IncludeNetWorth = account.IncludeNetWorth,
                Latitude = account.Latitude,
                Longitude = account.Longitude,
                LiabilityDirection = account.LiabilityDirection,
                LiabilityType = account.LiabilityType,
                MonthlyPaymentDate = account.MonthlyPaymentDate,
                Name = account.Name,
                Notes = account.Notes,
                Order = account.Order ?? 0,
                OpeningBalance = account.OpeningBalance,
                OpeningBalanceDate = account.OpeningBalanceDate,
                Type = MapAccountType(account.Type),
                VirtualBalance = account.VirtualBalance,
                ZoomLevel = account.ZoomLevel
            };
        }

        public static ICollection<FireflyAccount> MapToFireflyAccountCollection(this IEnumerable<FireflyDataContainer<FireflyAccountAttributes>> fireflyDataContainers)
        {
            return fireflyDataContainers.Select(dc => dc.MapToFireflyAccount()).ToList();
        }

        public static FireflyApiTransaction MapToFireflyApiTransaction(this FireflyTransaction transaction)
        {
            return new FireflyApiTransaction
            {
                Amount = transaction.Amount,
                BillId = transaction.BillId,
                BillName = transaction.BillName,
                BookDate = transaction.BookDate,
                BudgetId = transaction.BudgetId,
                BudgetName = transaction.BudgetName,
                BunqPaymentId = transaction.BunqPaymentId,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.CategoryName,
                CurrencyCode = transaction.CurrencyCode,
                CurrencyDecimalPlaces = transaction.CurrencyDecimalPlaces,
                CurrencyId = transaction.CurrencyId,
                CurrencyName = transaction.CurrencyName,
                CurrencySymbol = transaction.CurrencySymbol,
                Date = transaction.Date,
                Description = transaction.Description,
                DestinationIban = transaction.DestinationIban,
                DestinationId = transaction.DestinationId,
                DestinationName = transaction.DestinationName,
                DestinationType = transaction.DestinationType,
                DueDate = transaction.DueDate,
                ExternalId = transaction.ExternalId,
                ForeignAmount = double.TryParse(transaction.ForeignAmount, out var value) ? value : 0,
                ForeignCurrencyCode = transaction.ForeignCurrencyCode,
                ForeignCurrencyDecimalPlaces = transaction.ForeignCurrencyDecimalPlaces,
                ForeignCurrencyId = transaction.ForeignCurrencyId,
                ForeignCurrencySymbol = transaction.ForeignCurrencySymbol,
                ImportHashV2 = transaction.ImportHashV2,
                InterestDate = transaction.InterestDate,
                InternalReference = transaction.InternalReference,
                Latitude = transaction.Latitude,
                Longitude = transaction.Longitude,
                Notes = transaction.Notes,
                Order = transaction.Order,
                OriginalSource = transaction.OriginalSource,
                PaymentDate = transaction.PaymentDate,
                ProcessDate = transaction.ProcessDate,
                Reconciled = transaction.Reconciled ?? false,
                RecurrenceCount = transaction.RecurrenceCount,
                RecurrenceId = transaction.RecurrenceId,
                RecurrenceTotal = transaction.RecurrenceTotal,
                SepaBatchId = transaction.SepaBatchId,
                SepaCc = transaction.SepaCc,
                SepaCi = transaction.SepaCi,
                SepaCountry = transaction.SepaCountry,
                SepaCtId = transaction.SepaCtId,
                SepaCtOp = transaction.SepaCtOp,
                SepaDb = transaction.SepaDb,
                SepaEp = transaction.SepaEp,
                SourceIban = transaction.SourceIban,
                SourceId = transaction.SourceId,
                SourceName = transaction.SourceName,
                SourceType = transaction.SourceType,
                Tags = transaction.Tags,
                TransactionJournalId = transaction.TransactionJournalId,
                Type = MapApiTransactionType(transaction.Type),
                User = transaction.User,
                ZoomLevel = transaction.ZoomLevel
            };
        }

        public static FireflyTransaction MapToFireflyTransaction(this FireflyDataContainer<FireflyTransactionAttributes> fireflyDataContainer)
        {
            var transaction = fireflyDataContainer.Attributes.Transactions.FirstOrDefault();
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            return new FireflyTransaction
            {
                Amount = transaction.Amount,
                BillId = transaction.BillId,
                BillName = transaction.BillName,
                BookDate = transaction.BookDate,
                BudgetId = transaction.BudgetId,
                BudgetName = transaction.BudgetName,
                BunqPaymentId = transaction.BunqPaymentId,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.CategoryName,
                CurrencyCode = transaction.CurrencyCode,
                CurrencyDecimalPlaces = transaction.CurrencyDecimalPlaces,
                CurrencyId = transaction.CurrencyId,
                CurrencyName = transaction.CurrencyName,
                CurrencySymbol = transaction.CurrencySymbol,
                Date = transaction.Date,
                Description = transaction.Description,
                DestinationIban = transaction.DestinationIban,
                DestinationId = transaction.DestinationId,
                DestinationName = transaction.DestinationName,
                DestinationType = transaction.DestinationType,
                DueDate = transaction.DueDate,
                ExternalId = transaction.ExternalId,
                ForeignAmount = transaction.ForeignAmount.ToString(),
                ForeignCurrencyCode = transaction.ForeignCurrencyCode,
                ForeignCurrencyDecimalPlaces = transaction.ForeignCurrencyDecimalPlaces,
                ForeignCurrencyId = transaction.ForeignCurrencyId,
                ForeignCurrencySymbol = transaction.ForeignCurrencySymbol,
                ImportHashV2 = transaction.ImportHashV2,
                Id = fireflyDataContainer.Id,
                InterestDate = transaction.InterestDate,
                InternalReference = transaction.InternalReference,
                Latitude = transaction.Latitude,
                Longitude = transaction.Longitude,
                Notes = transaction.Notes,
                Order = transaction.Order,
                OriginalSource = transaction.OriginalSource,
                PaymentDate = transaction.PaymentDate,
                ProcessDate = transaction.ProcessDate,
                Reconciled = transaction.Reconciled,
                RecurrenceCount = transaction.RecurrenceCount,
                RecurrenceId = transaction.RecurrenceId,
                RecurrenceTotal = transaction.RecurrenceTotal,
                SepaBatchId = transaction.SepaBatchId,
                SepaCc = transaction.SepaCc,
                SepaCi = transaction.SepaCi,
                SepaCountry = transaction.SepaCountry,
                SepaCtId = transaction.SepaCtId,
                SepaCtOp = transaction.SepaCtOp,
                SepaDb = transaction.SepaDb,
                SepaEp = transaction.SepaEp,
                SourceIban = transaction.SourceIban,
                SourceId = transaction.SourceId,
                SourceName = transaction.SourceName,
                SourceType = transaction.SourceType,
                Tags = transaction.Tags,
                TransactionJournalId = transaction.TransactionJournalId,
                Type = MapTransactionType(transaction.Type),
                User = transaction.User,
                ZoomLevel = transaction.ZoomLevel
            };
        }

        public static ICollection<FireflyTransaction> MapToFireflyTransactionCollection(this IEnumerable<FireflyDataContainer<FireflyTransactionAttributes>> fireflyDataContainers)
        {
            return fireflyDataContainers.Select(dc => dc.MapToFireflyTransaction()).ToList();
        }

        private static AccountType MapAccountType(string type)
        {
            switch (type)
            {
                case "expense":
                    return AccountType.Expense;
                case "asset":
                    return AccountType.Asset;
                case "liability":
                    return AccountType.Liability;
                case "revenue":
                    return AccountType.Revenue;
            }

            return AccountType.Unknown;
        }

        private static string MapApiTransactionType(TransactionType type)
        {
            switch (type)
            {
                case TransactionType.Withdrawal:
                    return "withdrawal";
                case TransactionType.Deposit:
                    return "deposit";
                case TransactionType.Transfer:
                    return "transfer";
                default:
                    return "";
            }
        }

        private static TransactionType MapTransactionType(string type)
        {
            switch (type)
            {
                case "withdrawal":
                    return TransactionType.Withdrawal;
                case "deposit":
                    return TransactionType.Deposit;
                case "transfer":
                    return TransactionType.Transfer;
                default:
                    return TransactionType.Unknown;
            }
        }

        #endregion

        #endregion
    }
}