namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System.Text.Json.Serialization;

public abstract class FireflySingleResponseBase<TAttributes> : FireflyResponseBase<TAttributes>
    where TAttributes : FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("data")]
    public abstract FireflyDataContainer<TAttributes> Data { get; set; }

    #endregion
}