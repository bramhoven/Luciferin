namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Text.Json.Serialization;

public class NordigenAccountDetailsResponse
{
    #region Properties

    [JsonPropertyName("account")]
    public NordigenAccountDetails Account { get; set; }

    #endregion
}