namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class NordigenAccountBalanceResponse
{
    #region Properties

    [JsonPropertyName("balances")]
    public ICollection<NordigenAccountBalance> Balances { get; set; }

    #endregion
}