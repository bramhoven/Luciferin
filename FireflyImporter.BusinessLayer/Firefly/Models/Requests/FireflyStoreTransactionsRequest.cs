using System.Collections.Generic;
using FireflyImporter.BusinessLayer.Firefly.Models.Shared;
using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Requests
{
    public class FireflyStoreTransactionsRequest
    {
        #region Properties

        [JsonProperty("apply_rules")]
        public bool ApplyRules { get; set; }

        [JsonProperty("error_if_duplicate_hash")]
        public bool ErrorIfDuplicateHash { get; set; }

        [JsonProperty("fire_webhooks")]
        public bool FireWebhooks { get; set; }

        [JsonProperty("group_title")]
        public string GroupTitle { get; set; }

        [JsonProperty("transactions")]
        public ICollection<FireflyApiTransaction> Transactions { get; set; }

        #endregion
    }
}