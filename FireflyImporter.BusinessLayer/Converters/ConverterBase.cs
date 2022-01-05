using System.Collections.Generic;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.BusinessLayer.Converters
{
    public abstract class ConverterBase : ITransactionConverter
    {
        #region Methods

        /// <inheritdoc />
        public virtual FireflyTransaction ConvertTransaction(Transaction transaction, string tag)
        {
            var amount = transaction.TransactionAmount.Amount.Replace("-", "");
            var decimalPlacesMissing = 24 - amount.Split('.')[1].Length;

            var fireflyTransaction = new FireflyTransaction
            {
                RequisitionIban = transaction.RequisitorIban,
                Amount = $"{amount}{new string('0', decimalPlacesMissing)}",
                Date = transaction.BookingDate,
                ExternalId = transaction.TransactionId ?? transaction.EntryReference
            };

            if (!string.IsNullOrWhiteSpace(tag))
                fireflyTransaction.Tags = new List<string> { tag };

            return FillTransactions(fireflyTransaction, transaction);
        }

        /// <summary>
        /// Fills the transaction with correct data.
        /// </summary>
        /// <param name="fireflyTransaction">The new Firefly transaction.</param>
        /// <param name="transaction">The transaction to convert.</param>
        /// <returns></returns>
        protected abstract FireflyTransaction FillTransactions(FireflyTransaction fireflyTransaction, Transaction transaction);

        #endregion
    }
}