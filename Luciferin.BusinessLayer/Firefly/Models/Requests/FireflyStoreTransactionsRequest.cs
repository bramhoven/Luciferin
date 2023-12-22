namespace Luciferin.BusinessLayer.Firefly.Models.Requests;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shared;

public class FireflyStoreTransactionsRequest
{
    #region Properties

    [JsonPropertyName("apply_rules")]
    public bool ApplyRules { get; set; }

    [JsonPropertyName("error_if_duplicate_hash")]
    public bool ErrorIfDuplicateHash { get; set; }

    [JsonPropertyName("fire_webhooks")]
    public bool FireWebhooks { get; set; }

    [JsonPropertyName("group_title")]
    public string GroupTitle { get; set; }

    [JsonPropertyName("transactions")]
    public ICollection<FireflyApiTransaction> Transactions { get; set; }

    #endregion
}