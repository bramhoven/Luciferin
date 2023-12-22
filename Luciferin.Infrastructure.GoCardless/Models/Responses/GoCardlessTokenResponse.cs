namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessTokenResponse
{
    [JsonPropertyName("access")]
    public string Access { get; set; }

    [JsonPropertyName("access_expires")]
    public int AccessExpires { get; set; }

    [JsonPropertyName("refresh")]
    public string Refresh { get; set; }

    [JsonPropertyName("refresh_expires")]
    public int RefreshExpires { get; set; }
}