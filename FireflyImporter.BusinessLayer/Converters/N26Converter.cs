using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.BusinessLayer.Converters
{
    public class N26Converter : ConverterBase
    {
        #region Methods

        /// <inheritdoc />
        public override FireflyTransaction ConvertTransaction(Transaction transaction)
        {
            var fireflyTransaction = base.ConvertTransaction(transaction);
            fireflyTransaction.ExternalId = transaction.TransactionId;

            var text = transaction.CreditorName ?? transaction.DebtorName;
            fireflyTransaction.Description = text;
            fireflyTransaction.Notes = text;

            return fireflyTransaction;
        }

        #endregion
    }
}