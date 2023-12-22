namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessTransaction
{
    [JsonPropertyName("bankTransactionCode")]
    public string BankTransactionCode { get; set; }

    [JsonPropertyName("bookingDate")]
    public DateTime BookingDate { get; set; }

    [JsonPropertyName("creditorAccount")]
    public GoCardlessCreditorAccount CreditorAccount { get; set; }

    [JsonPropertyName("creditorName")]
    public string CreditorName { get; set; }

    [JsonPropertyName("debtorAccount")]
    public GoCardlessCreditorAccount DebtorAccount { get; set; }

    [JsonPropertyName("DebtorName")]
    public string DebtorName { get; set; }

    [JsonPropertyName("entryReference")]
    public string EntryReference { get; set; }

    [JsonPropertyName("remittanceInformationUnstructured")]
    public string RemittanceInformationUnstructured { get; set; }

    [JsonPropertyName("transactionAmount")]
    public TransactionAmount TransactionAmount { get; set; }

    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; }

    [JsonPropertyName("valueDate")]
    public DateTime ValueDate { get; set; }
}