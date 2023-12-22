namespace Luciferin.Infrastructure.GoCardless.Models.Responses;

using System.Text.Json.Serialization;

public class GoCardlessCreditorAccount
{
    [JsonPropertyName("iban")]
    public string Iban { get; set; }

    [JsonPropertyName("bban")]
    public string Bban { get; set; }
}