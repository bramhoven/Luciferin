using System.Collections.Generic;

namespace FireflyWebImporter.BusinessLayer.Firefly.Models.Responses.Accounts
{
    public class FireflyAccountResponse : FireflyResponseBase<FireflyAccountAttributes>
    {
        #region Properties

        /// <inheritdoc />
        public override ICollection<FireflyDataContainer<FireflyAccountAttributes>> Data { get; set; }

        #endregion
    }
}