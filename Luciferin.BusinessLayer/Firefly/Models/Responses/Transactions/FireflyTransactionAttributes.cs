namespace Luciferin.BusinessLayer.Firefly.Models.Responses.Transactions;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shared;

public class FireflyTransactionAttributes : FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("group_title")]
    public string GroupTitle { get; set; }

    [JsonPropertyName("transactions")]
    public ICollection<FireflyApiTransaction> Transactions { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; }

    #endregion
}