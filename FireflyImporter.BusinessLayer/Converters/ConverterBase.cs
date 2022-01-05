using System.Collections.Generic;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.BusinessLayer.Converters
{
    public class ConverterBase : ITransactionConverter
    {
        #region Methods

        /// <inheritdoc />
        public virtual FireflyTransaction ConvertTransaction(Transaction transaction)
        {
            var amount = transaction.TransactionAmount.Amount.Replace("-", "");
            var decimalPlacesMissing = 24 - amount.Split('.')[1].Length;

            var fireflyTransaction = new FireflyTransaction
            {
                RequisitionIban = transaction.RequisitorIban,
                Amount = $"{amount}{new string('0', decimalPlacesMissing)}",
                Date = transaction.BookingDate,
                ExternalId = transaction.TransactionId ?? transaction.EntryReference,
                Tags = new List<string>()
            };

            return fireflyTransaction;
        }

        #endregion
    }
}