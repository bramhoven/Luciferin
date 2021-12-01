using System;
using System.Collections.Generic;
using System.Linq;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Responses;
using FireflyWebImporter.BusinessLayer.Firefly.Models.Responses.Transactions;

namespace FireflyWebImporter.BusinessLayer.Firefly.Helpers
{
    public static class FireflyResponseMapper
    {
        #region Methods

        #region Static Methods

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
                ForeignAmount = transaction.ForeignAmount,
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

        private static TransactionType MapTransactionType(string type)
        {
            switch (type)
            {
                case "withdrawal":
                    return TransactionType.Withdrawal;
                case "deposit":
                    return TransactionType.Deposit;
                default:
                    return TransactionType.Unknown;
            }
        }

        #endregion

        #endregion
    }
}