using Luciferin.BusinessLayer.Configuration.Interfaces;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Converters
{
    internal class N26Converter : ConverterBase
    {
        #region Constructors

        /// <inheritdoc />
        public N26Converter(ICompositeConfiguration configuration) : base(configuration) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override FireflyTransaction FillTransactions(FireflyTransaction fireflyTransaction, Transaction transaction)
        {
            fireflyTransaction.ExternalId = transaction.TransactionId;

            var text = transaction.CreditorName ?? transaction.DebtorName;
            fireflyTransaction.Description = text;
            fireflyTransaction.Notes = GetNotes(transaction, text);

            return fireflyTransaction;
        }

        #endregion
    }
}