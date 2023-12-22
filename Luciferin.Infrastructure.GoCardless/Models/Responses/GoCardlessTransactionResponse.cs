namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessTransactionResponse
{
    [JsonPropertyName("transactions")]
    public NordigenTransactionResponseTransactions Transactions { get; set; }
}

public class NordigenTransactionResponseTransactions
{
    [JsonPropertyName("booked")]
    public ICollection<GoCardlessTransaction> Booked { get; set; }

    [JsonPropertyName("pending")]
    public ICollection<GoCardlessTransaction> Pending { get; set; }
}