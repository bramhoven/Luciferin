namespace Luciferin.Infrastructure.GoCardless.Models.Requests;

using System.Text.Json.Serialization;

public class GoCardlessEndUserAgreementRequest
{
    [JsonPropertyName("access_scope")]
    public ICollection<string> AccessScopes { get; set; }

    [JsonPropertyName("access_valid_for_days")]
    public int AccessValidForDays { get; set; }

    [JsonPropertyName("institution_id")]
    public string InstitutionId { get; set; }

    [JsonPropertyName("max_historical_days")]
    public int MaxHistoricalDays { get; set; }
}