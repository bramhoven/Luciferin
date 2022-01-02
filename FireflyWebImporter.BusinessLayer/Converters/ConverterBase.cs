using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Converters
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
                Amount = $"{amount}{new string('0', decimalPlacesMissing)}",
                Date = transaction.BookingDate,
            };

            return fireflyTransaction;
        }

        #endregion
    }
}