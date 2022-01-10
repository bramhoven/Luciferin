using System.Collections.Generic;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses.Accounts
{
    public class FireflyAccountCollectionResponse : FireflyCollectionResponseBase<FireflyAccountAttributes>
    {
        #region Properties

        /// <inheritdoc />
        public override ICollection<FireflyDataContainer<FireflyAccountAttributes>> Data { get; set; }

        #endregion
    }
}