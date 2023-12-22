namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessEndUserAgreementResponse
{
    [JsonPropertyName("accepted")]
    public DateTime? Accepted { get; set; }

    [JsonPropertyName("access_scope")]
    public List<string> AccessScope { get; set; }

    [JsonPropertyName("access_valid_for_days")]
    public int AccessValidForDays { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("institution_id")]
    public string InstitutionId { get; set; }

    [JsonPropertyName("max_historical_days")]
    public int MaxHistoricalDays { get; set; }
}