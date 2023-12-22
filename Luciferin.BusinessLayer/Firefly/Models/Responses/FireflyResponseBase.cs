namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System.Text.Json.Serialization;

public abstract class FireflyResponseBase<TAttributes>
    where TAttributes : FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("links")]
    public FireflyLinks Links { get; set; }

    [JsonPropertyName("meta")]
    public FireflyMeta Meta { get; set; }

    #endregion
}