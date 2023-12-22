namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public abstract class FireflyCollectionResponseBase<TAttributes> : FireflyResponseBase<TAttributes>
    where TAttributes : FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("data")]
    public abstract ICollection<FireflyDataContainer<TAttributes>> Data { get; set; }

    #endregion
}