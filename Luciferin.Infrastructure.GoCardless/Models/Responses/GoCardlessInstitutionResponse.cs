﻿namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessInstitutionResponse
{
    [JsonPropertyName("bic")]
    public string Bic { get; set; }

    [JsonPropertyName("countries")]
    public ICollection<string> Countries { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("logo")]
    public string Logo { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("transaction_total_days")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int TransactionTotalDays { get; set; }
}