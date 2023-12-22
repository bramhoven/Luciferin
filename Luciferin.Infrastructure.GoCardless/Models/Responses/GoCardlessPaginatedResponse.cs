namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessPaginatedResponse<TNordigenData>
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("results")]
    public ICollection<TNordigenData> Results { get; set; }
}