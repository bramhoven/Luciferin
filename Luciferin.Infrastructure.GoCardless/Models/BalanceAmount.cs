namespace Luciferin.Infrastructure.GoCardless.Models;

using System.Text.Json.Serialization;

public class BalanceAmount
{
    [JsonPropertyName("amount")]
    public string Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}