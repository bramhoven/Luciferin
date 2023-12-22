namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Text.Json.Serialization;

public class NordigenCreditorAccount
{
    #region Properties

    [JsonPropertyName("iban")]
    public string Iban { get; set; }

    [JsonPropertyName("bban")]
    public string Bban { get; set; }

    #endregion
}