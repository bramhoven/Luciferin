using System.Collections.Generic;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses.Transactions
{
    public sealed class FireflyTransactionCollectionResponse : FireflyCollectionResponseBase<FireflyTransactionAttributes>
    {
        #region Properties

        /// <inheritdoc />
        public override ICollection<FireflyDataContainer<FireflyTransactionAttributes>> Data { get; set; }

        #endregion
    }
}