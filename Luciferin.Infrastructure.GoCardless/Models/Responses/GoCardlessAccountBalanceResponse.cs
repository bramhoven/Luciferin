namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessAccountBalanceResponse
{
    [JsonPropertyName("balances")]
    public ICollection<GoCardlessAccountBalance> Balances { get; set; }
}