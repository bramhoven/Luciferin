namespace Luciferin.BusinessLayer.Nordigen.Models;

using System.Text.Json.Serialization;

public class BalanceAmount
{
    #region Properties

    [JsonPropertyName("amount")]
    public string Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    #endregion
}