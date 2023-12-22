namespace Luciferin.Infrastructure.GoCardless.Models;

using System.Text.Json.Serialization;

public class Balance
{
    [JsonPropertyName("balanceAmount")]
    public BalanceAmount BalanceAmount { get; set; }

    [JsonPropertyName("balanceType")]
    public string BalanceType { get; set; }

    [JsonPropertyName("lastChangeDateTime")]
    public DateTime LastChangeDateTime { get; set; }
}