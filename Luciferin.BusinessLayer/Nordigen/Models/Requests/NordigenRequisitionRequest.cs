namespace Luciferin.BusinessLayer.Nordigen.Models.Requests;

using System.Text.Json.Serialization;

public class NordigenRequisitionRequest
{
    #region Properties

    [JsonPropertyName("agreement")]
    public string AgreementId { get; set; }

    [JsonPropertyName("institution_id")]
    public string InstitutionId { get; set; }

    [JsonPropertyName("redirect")]
    public string RedirectUrl { get; set; }

    [JsonPropertyName("reference")]
    public string Reference { get; set; }

    #endregion
}