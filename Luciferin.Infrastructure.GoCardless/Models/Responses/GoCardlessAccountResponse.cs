namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessAccountResponse
{
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("iban")]
    public string Iban { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("institution_id")]
    public string InstitutionId { get; set; }

    [JsonPropertyName("last_accessed")]
    public DateTime LastAccessed { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}