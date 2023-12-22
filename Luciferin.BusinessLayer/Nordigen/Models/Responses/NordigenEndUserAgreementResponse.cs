namespace Luciferin.BusinessLayer.Nordigen.Models.Responses;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class NordigenEndUserAgreementResponse
{
    #region Properties

    [JsonPropertyName("accepted")]
    public DateTime? Accepted { get; set; }

    [JsonPropertyName("access_scope")]
    public List<string> AccessScope { get; set; }

    [JsonPropertyName("access_valid_for_days")]
    public int AccessValidForDays { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("institution_id")]
    public string InstitutionId { get; set; }

    [JsonPropertyName("max_historical_days")]
    public int MaxHistoricalDays { get; set; }

    #endregion
}