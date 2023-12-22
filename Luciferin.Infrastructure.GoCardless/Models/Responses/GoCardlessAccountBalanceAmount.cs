namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessAccountBalanceAmount
{
    [JsonPropertyName("amount")]
    public string Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}