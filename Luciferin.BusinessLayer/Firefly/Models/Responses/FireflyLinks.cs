namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System.Text.Json.Serialization;

public class FireflyLinks
{
    #region Properties

    [JsonPropertyName("first")]
    public string First { get; set; }

    [JsonPropertyName("last")]
    public string Last { get; set; }

    [JsonPropertyName("self")]
    public string Self { get; set; }

    #endregion
}