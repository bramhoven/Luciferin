using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Firefly.Models.Responses.Transactions
{
    public class FireflyTransactionAttributes : FireflyAttributesBase
    {
        #region Properties

        [JsonProperty("group_title")]
        public string GroupTitle { get; set; }

        [JsonProperty("transactions")]
        public ICollection<FireflyTransactionResponseTransaction> Transactions { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        #endregion
    }
}