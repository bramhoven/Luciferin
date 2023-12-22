using System.Text.Json.Serialization;

namespace Luciferin.Infrastructure.GoCardless.Models.Requests;

public class GoCardlessTokenRequest
{
    [JsonPropertyName("secret_id")]
    public string SecretId { get; set; }

    [JsonPropertyName("secret_key")]
    public string SecretKey { get; set; }
}