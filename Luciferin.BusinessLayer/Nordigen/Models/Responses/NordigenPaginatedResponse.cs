namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class NordigenPaginatedResponse<TNordigenData>
{
    #region Properties

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("results")]
    public ICollection<TNordigenData> Results { get; set; }

    #endregion
}