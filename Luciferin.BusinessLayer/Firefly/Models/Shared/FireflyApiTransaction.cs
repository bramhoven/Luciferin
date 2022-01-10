using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Firefly.Models.Shared
{
    public class FireflyApiTransaction
    {
        #region Properties

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("bill_name")]
        public string BillName { get; set; }

        [JsonProperty("book_date")]
        public DateTime? BookDate { get; set; }

        [JsonProperty("budget_id")]
        public string BudgetId { get; set; }

        [JsonProperty("budget_name")]
        public string BudgetName { get; set; }

        [JsonProperty("bunq_payment_id")]
        public string BunqPaymentId { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_decimal_places")]
        public int? CurrencyDecimalPlaces { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("currency_name")]
        public string CurrencyName { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("destination_iban")]
        public string DestinationIban { get; set; }

        [JsonProperty("destination_id", NullValueHandling = NullValueHandling.Ignore)]
        public int DestinationId { get; set; }

        [JsonProperty("destination_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationName { get; set; }

        [JsonProperty("destination_type")]
        public string DestinationType { get; set; }

        [JsonProperty("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("foreign_amount")]
        public double? ForeignAmount { get; set; }

        [JsonProperty("foreign_currency_code")]
        public string ForeignCurrencyCode { get; set; }

        [JsonProperty("foreign_currency_decimal_places")]
        public int ForeignCurrencyDecimalPlaces { get; set; }

        [JsonProperty("foreign_currency_id")]
        public string ForeignCurrencyId { get; set; }

        [JsonProperty("foreign_currency_symbol")]
        public string ForeignCurrencySymbol { get; set; }

        [JsonProperty("import_hash_v2")]
        public string ImportHashV2 { get; set; }

        [JsonProperty("interest_date")]
        public DateTime? InterestDate { get; set; }

        [JsonProperty("internal_reference")]
        public string InternalReference { get; set; }

        [JsonProperty("invoice_date")]
        public DateTime? InvoiceDate { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("original_source")]
        public string OriginalSource { get; set; }

        [JsonProperty("payment_date")]
        public DateTime? PaymentDate { get; set; }

        [JsonProperty("process_date")]
        public DateTime? ProcessDate { get; set; }

        [JsonProperty("reconciled")]
        public bool? Reconciled { get; set; }

        [JsonProperty("recurrence_count")]
        public int? RecurrenceCount { get; set; }

        [JsonProperty("recurrence_id")]
        public int? RecurrenceId { get; set; }

        [JsonProperty("recurrence_total")]
        public int? RecurrenceTotal { get; set; }

        [JsonProperty("sepa_batch_id")]
        public string SepaBatchId { get; set; }

        [JsonProperty("sepa_cc")]
        public string SepaCc { get; set; }

        [JsonProperty("sepa_ci")]
        public string SepaCi { get; set; }

        [JsonProperty("sepa_country")]
        public string SepaCountry { get; set; }

        [JsonProperty("sepa_ct_id")]
        public string SepaCtId { get; set; }

        [JsonProperty("sepa_ct_op")]
        public string SepaCtOp { get; set; }

        [JsonProperty("sepa_db")]
        public string SepaDb { get; set; }

        [JsonProperty("sepa_ep")]
        public string SepaEp { get; set; }

        [JsonProperty("source_iban")]
        public string SourceIban { get; set; }

        [JsonProperty("source_id", NullValueHandling = NullValueHandling.Ignore)]
        public int SourceId { get; set; }

        [JsonProperty("source_name", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceName { get; set; }

        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        [JsonProperty("tags")]
        public ICollection<string> Tags { get; set; }

        [JsonProperty("transaction_journal_id")]
        public string TransactionJournalId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("zoom_level")]
        public int? ZoomLevel { get; set; }

        #endregion
    }
}