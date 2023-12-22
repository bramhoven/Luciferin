namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessAccountBalance
{
    [JsonPropertyName("balanceAmount")]
    public BalanceAmount BalanceAmount { get; set; }

    [JsonPropertyName("balanceType")]
    public string BalanceType { get; set; }

    [JsonPropertyName("lastChangeDateTime")]
    public DateTime LastChangeDateTime { get; set; }
}