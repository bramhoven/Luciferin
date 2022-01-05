using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.BusinessLayer.Converters
{
    internal class DefaultConverter : ConverterBase
    {
        #region Methods

        /// <inheritdoc />
        protected override FireflyTransaction FillTransactions(FireflyTransaction fireflyTransaction, Transaction transaction)
        {
            fireflyTransaction.ExternalId = transaction.TransactionId;

            var text = transaction.CreditorName ?? transaction.DebtorName;
            fireflyTransaction.Description = text;
            fireflyTransaction.Notes = text;

            return fireflyTransaction;
        }

        #endregion
    }
}