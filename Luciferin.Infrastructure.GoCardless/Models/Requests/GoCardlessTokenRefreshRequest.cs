using Newtonsoft.Json;

namespace Luciferin.Infrastructure.GoCardless.Models.Requests;

using System.Text.Json.Serialization;

public class GoCardlessTokenRefreshRequest
{
    [JsonPropertyName("refresh")]
    public string Refresh { get; set; }
}