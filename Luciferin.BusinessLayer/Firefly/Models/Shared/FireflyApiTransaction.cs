namespace Luciferin.BusinessLayer.Firefly.Models.Shared;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class FireflyApiTransaction
{
    #region Properties

    [JsonPropertyName("amount")]
    public string Amount { get; set; }

    [JsonPropertyName("bill_id")]
    public string BillId { get; set; }

    [JsonPropertyName("bill_name")]
    public string BillName { get; set; }

    [JsonPropertyName("book_date")]
    public DateTime? BookDate { get; set; }

    [JsonPropertyName("budget_id")]
    public string BudgetId { get; set; }

    [JsonPropertyName("budget_name")]
    public string BudgetName { get; set; }

    [JsonPropertyName("bunq_payment_id")]
    public string BunqPaymentId { get; set; }

    [JsonPropertyName("category_id")]
    public string CategoryId { get; set; }

    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("currency_decimal_places")]
    public int? CurrencyDecimalPlaces { get; set; }

    [JsonPropertyName("currency_id")]
    public string CurrencyId { get; set; }

    [JsonPropertyName("currency_name")]
    public string CurrencyName { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string CurrencySymbol { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("destination_iban")]
    public string DestinationIban { get; set; }

    [JsonPropertyName("destination_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int DestinationId { get; set; }

    [JsonPropertyName("destination_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string DestinationName { get; set; }

    [JsonPropertyName("destination_type")]
    public string DestinationType { get; set; }

    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("external_id")]
    public string ExternalId { get; set; }

    [JsonPropertyName("foreign_amount")]
    public double? ForeignAmount { get; set; }

    [JsonPropertyName("foreign_currency_code")]
    public string ForeignCurrencyCode { get; set; }

    [JsonPropertyName("foreign_currency_decimal_places")]
    public int ForeignCurrencyDecimalPlaces { get; set; }

    [JsonPropertyName("foreign_currency_id")]
    public string ForeignCurrencyId { get; set; }

    [JsonPropertyName("foreign_currency_symbol")]
    public string ForeignCurrencySymbol { get; set; }

    [JsonPropertyName("import_hash_v2")]
    public string ImportHashV2 { get; set; }

    [JsonPropertyName("interest_date")]
    public DateTime? InterestDate { get; set; }

    [JsonPropertyName("internal_reference")]
    public string InternalReference { get; set; }

    [JsonPropertyName("invoice_date")]
    public DateTime? InvoiceDate { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("original_source")]
    public string OriginalSource { get; set; }

    [JsonPropertyName("payment_date")]
    public DateTime? PaymentDate { get; set; }

    [JsonPropertyName("process_date")]
    public DateTime? ProcessDate { get; set; }

    [JsonPropertyName("reconciled")]
    public bool? Reconciled { get; set; }

    [JsonPropertyName("recurrence_count")]
    public int? RecurrenceCount { get; set; }

    [JsonPropertyName("recurrence_id")]
    public int? RecurrenceId { get; set; }

    [JsonPropertyName("recurrence_total")]
    public int? RecurrenceTotal { get; set; }

    [JsonPropertyName("sepa_batch_id")]
    public string SepaBatchId { get; set; }

    [JsonPropertyName("sepa_cc")]
    public string SepaCc { get; set; }

    [JsonPropertyName("sepa_ci")]
    public string SepaCi { get; set; }

    [JsonPropertyName("sepa_country")]
    public string SepaCountry { get; set; }

    [JsonPropertyName("sepa_ct_id")]
    public string SepaCtId { get; set; }

    [JsonPropertyName("sepa_ct_op")]
    public string SepaCtOp { get; set; }

    [JsonPropertyName("sepa_db")]
    public string SepaDb { get; set; }

    [JsonPropertyName("sepa_ep")]
    public string SepaEp { get; set; }

    [JsonPropertyName("source_iban")]
    public string SourceIban { get; set; }

    [JsonPropertyName("source_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int SourceId { get; set; }

    [JsonPropertyName("source_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SourceName { get; set; }

    [JsonPropertyName("source_type")]
    public string SourceType { get; set; }

    [JsonPropertyName("tags")]
    public ICollection<string> Tags { get; set; }

    [JsonPropertyName("transaction_journal_id")]
    public string TransactionJournalId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; }

    [JsonPropertyName("zoom_level")]
    public int? ZoomLevel { get; set; }

    #endregion
}