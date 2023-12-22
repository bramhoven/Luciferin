namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System.Text.Json.Serialization;

public class FireflyDataContainer<TAttributes>
    where TAttributes : FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("attributes")]
    public TAttributes Attributes { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("links")]
    public FireflyLinks Links { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    #endregion
}