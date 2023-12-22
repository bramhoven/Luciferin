namespace Luciferin.BusinessLayer.Firefly.Models.Responses.Accounts;

using System;
using System.Text.Json.Serialization;

public class FireflyAccountAttributes : FireflyAttributesBase
{
    #region Properties

    [JsonPropertyName("account_number")]
    public string AccountNumber { get; set; }

    [JsonPropertyName("account_role")]
    public string AccountRole { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("bic")]
    public string Bic { get; set; }

    [JsonPropertyName("credit_card_type")]
    public string CreditCardType { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("currency_decimal_places")]
    public int CurrencyDecimalPlaces { get; set; }

    [JsonPropertyName("currency_id")]
    public string CurrencyId { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string CurrencySymbol { get; set; }

    [JsonPropertyName("current_balance")]
    public string CurrentBalance { get; set; }

    [JsonPropertyName("current_balance_date")]
    public DateTime? CurrentBalanceDate { get; set; }

    [JsonPropertyName("current_debt")]
    public string CurrentDebt { get; set; }

    [JsonPropertyName("iban")]
    public string Iban { get; set; }

    [JsonPropertyName("include_net_worth")]
    public bool IncludeNetWorth { get; set; }

    [JsonPropertyName("interest")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Interest { get; set; }

    [JsonPropertyName("interest_period")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string InterestPeriod { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("liability_direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string LiabilityDirection { get; set; }

    [JsonPropertyName("liability_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string LiabilityType { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("monthly_payment_date")]
    public DateTime? MonthlyPaymentDate { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("opening_balance")]
    public string OpeningBalance { get; set; }

    [JsonPropertyName("opening_balance_date")]
    public DateTime? OpeningBalanceDate { get; set; }

    [JsonPropertyName("order")]
    public int? Order { get; set; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; set; }

    [JsonPropertyName("virtual_balance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string VirtualBalance { get; set; }

    [JsonPropertyName("zoom_level")]
    public int? ZoomLevel { get; set; }

    #endregion
}