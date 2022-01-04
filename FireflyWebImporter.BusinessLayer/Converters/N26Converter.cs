using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Converters
{
    public class N26Converter : ConverterBase
    {
        #region Methods

        /// <inheritdoc />
        public override FireflyTransaction ConvertTransaction(Transaction transaction)
        {
            var fireflyTransaction = base.ConvertTransaction(transaction);
            fireflyTransaction.ExternalId = transaction.EntryReference;

            var text = transaction.CreditorName ?? transaction.DebtorName;
            fireflyTransaction.Description = text;
            fireflyTransaction.Notes = text;

            return fireflyTransaction;
        }

        #endregion
    }
}