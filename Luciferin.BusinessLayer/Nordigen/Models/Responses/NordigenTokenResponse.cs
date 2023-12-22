namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System.Text.Json.Serialization;

public class NordigenTokenResponse
{
    #region Properties

    [JsonPropertyName("access")]
    public string Access { get; set; }

    [JsonPropertyName("access_expires")]
    public int AccessExpires { get; set; }

    [JsonPropertyName("refresh")]
    public string Refresh { get; set; }

    [JsonPropertyName("refresh_expires")]
    public int RefreshExpires { get; set; }

    #endregion
}