using System;
using Newtonsoft.Json;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses.Accounts
{
    public class FireflyAccountAttributes : FireflyAttributesBase
    {
        #region Properties

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("account_role")]
        public string AccountRole { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("bic")]
        public string Bic { get; set; }

        [JsonProperty("credit_card_type")]
        public string CreditCardType { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_decimal_places")]
        public int CurrencyDecimalPlaces { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("current_balance")]
        public string CurrentBalance { get; set; }

        [JsonProperty("current_balance_date")]
        public DateTime? CurrentBalanceDate { get; set; }

        [JsonProperty("current_debt")]
        public string CurrentDebt { get; set; }

        [JsonProperty("iban")]
        public string Iban { get; set; }

        [JsonProperty("include_net_worth")]
        public bool IncludeNetWorth { get; set; }

        [JsonProperty("interest", NullValueHandling = NullValueHandling.Ignore)]
        public string Interest { get; set; }

        [JsonProperty("interest_period", NullValueHandling = NullValueHandling.Ignore)]
        public string InterestPeriod { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("liability_direction", NullValueHandling = NullValueHandling.Ignore)]
        public string LiabilityDirection { get; set; }

        [JsonProperty("liability_type", NullValueHandling = NullValueHandling.Ignore)]
        public string LiabilityType { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("monthly_payment_date")]
        public DateTime? MonthlyPaymentDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("opening_balance")]
        public string OpeningBalance { get; set; }

        [JsonProperty("opening_balance_date")]
        public DateTime? OpeningBalanceDate { get; set; }

        [JsonProperty("order")]
        public int? Order { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("virtual_balance", NullValueHandling = NullValueHandling.Ignore)]
        public string VirtualBalance { get; set; }

        [JsonProperty("zoom_level")]
        public int? ZoomLevel { get; set; }

        #endregion
    }
}