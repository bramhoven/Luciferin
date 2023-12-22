namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System;
using System.Text.Json.Serialization;

public class NordigenAccountBalance
{
    #region Properties

    [JsonPropertyName("balanceAmount")]
    public BalanceAmount BalanceAmount { get; set; }

    [JsonPropertyName("balanceType")]
    public string BalanceType { get; set; }

    [JsonPropertyName("lastChangeDateTime")]
    public DateTime LastChangeDateTime { get; set; }

    #endregion
}