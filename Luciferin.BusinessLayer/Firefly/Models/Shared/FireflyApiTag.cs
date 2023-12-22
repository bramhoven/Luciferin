namespace Luciferin.BusinessLayer.Firefly.Models.Shared;

using System;
using System.Text.Json.Serialization;

public class FireflyApiTag
{
    #region Properties

    [JsonPropertyName("created_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime Created { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("latitude")]
    public long Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; }

    [JsonPropertyName("updated_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Updated { get; set; }

    [JsonPropertyName("zoom_level")]
    public int ZoomLevel { get; set; }

    #endregion
}