namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System;
using System.Text.Json.Serialization;

public class NordigenTransaction
{
    #region Properties

    [JsonPropertyName("bankTransactionCode")]
    public string BankTransactionCode { get; set; }

    [JsonPropertyName("bookingDate")]
    public DateTime BookingDate { get; set; }

    [JsonPropertyName("creditorAccount")]
    public NordigenCreditorAccount CreditorAccount { get; set; }

    [JsonPropertyName("creditorName")]
    public string CreditorName { get; set; }

    [JsonPropertyName("debtorAccount")]
    public NordigenCreditorAccount DebtorAccount { get; set; }

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

    #endregion
}