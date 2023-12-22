namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessAccountDetailsResponse
{
    [JsonPropertyName("account")]
    public GoCardlessAccountDetails Account { get; set; }
}