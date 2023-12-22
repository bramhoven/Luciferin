namespace Luciferin.BusinessLayer.Firefly.Models.Responses;

using System;
using System.Text.Json.Serialization;

public class FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    #endregion
}