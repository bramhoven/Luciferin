namespace Luciferin.BusinessLayer.Nordigen.Models.Requests;

using System.Text.Json.Serialization;

public class NordigenTokenRefreshRequest
{
    #region Properties

    [JsonPropertyName("refresh")]
    public string Refresh { get; set; }

    #endregion
}