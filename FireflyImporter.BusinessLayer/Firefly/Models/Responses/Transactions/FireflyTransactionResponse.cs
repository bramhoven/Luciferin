﻿using System.Collections.Generic;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses.Transactions
{
    public sealed class FireflyTransactionResponse : FireflyResponseBase<FireflyTransactionAttributes>
    {
        #region Properties

        /// <inheritdoc />
        public override ICollection<FireflyDataContainer<FireflyTransactionAttributes>> Data { get; set; }

        #endregion
    }
}