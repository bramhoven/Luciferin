using System.Collections.Generic;
using FireflyImporter.BusinessLayer.Firefly.Models.Shared;
using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses.Transactions
{
    public class FireflyTransactionAttributes : FireflyAttributesBase
    {
        #region Properties

        [JsonProperty("group_title")]
        public string GroupTitle { get; set; }

        [JsonProperty("transactions")]
        public ICollection<FireflyApiTransaction> Transactions { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        #endregion
    }
}