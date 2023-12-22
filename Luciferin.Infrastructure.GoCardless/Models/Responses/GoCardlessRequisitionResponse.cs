using Newtonsoft.Json;

namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessRequisitionResponse
{
    [JsonPropertyName("accounts")] public List<string> Accounts { get; set; }

    [JsonPropertyName("account_selection")] public bool AccountSelection { get; set; }

    [JsonPropertyName("agreement")] public string Agreement { get; set; }

    [JsonPropertyName("created")] public DateTime Created { get; set; }

    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("institution_id")] public string InstitutionId { get; set; }

    [JsonPropertyName("link")] public string Link { get; set; }

    [JsonPropertyName("redirect")] public string Redirect { get; set; }

    [JsonPropertyName("reference")] public string Reference { get; set; }

    [JsonPropertyName("ssn")] public string Ssn { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("user_language")] public string UserLanguage { get; set; }
}