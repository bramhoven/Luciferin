namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Text.Json.Serialization;

public class NordigenTransactionAmount
{
    #region Properties

    [JsonPropertyName("amount")]
    public string Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    #endregion
}