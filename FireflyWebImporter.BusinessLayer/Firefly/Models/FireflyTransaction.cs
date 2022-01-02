using System;
using System.Collections.Generic;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Firefly.Models
{
    public class FireflyTransaction
    {
        #region Properties

        public string Amount { get; set; }

        public string BillId { get; set; }

        public string BillName { get; set; }

        public DateTime? BookDate { get; set; }

        public string BudgetId { get; set; }

        public string BudgetName { get; set; }

        public string BunqPaymentId { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CurrencyCode { get; set; }

        public int? CurrencyDecimalPlaces { get; set; }

        public string CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencySymbol { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string DestinationIban { get; set; }

        public int DestinationId { get; set; }

        public string DestinationName { get; set; }

        public string DestinationType { get; set; }

        public DateTime? DueDate { get; set; }

        public string ExternalId { get; set; }

        public string ForeignAmount { get; set; }

        public string ForeignCurrencyCode { get; set; }

        public int ForeignCurrencyDecimalPlaces { get; set; }

        public string ForeignCurrencyId { get; set; }

        public string ForeignCurrencySymbol { get; set; }

        public string Id { get; set; }

        public string ImportHashV2 { get; set; }

        public DateTime? InterestDate { get; set; }

        public string InternalReference { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Notes { get; set; }

        public int Order { get; set; }

        public string OriginalSource { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime? ProcessDate { get; set; }

        public bool? Reconciled { get; set; }

        public int? RecurrenceCount { get; set; }

        public int? RecurrenceId { get; set; }

        public int? RecurrenceTotal { get; set; }

        public string SepaBatchId { get; set; }

        public string SepaCc { get; set; }

        public string SepaCi { get; set; }

        public string SepaCountry { get; set; }

        public string SepaCtId { get; set; }

        public string SepaCtOp { get; set; }

        public string SepaDb { get; set; }

        public string SepaEp { get; set; }

        public string SourceIban { get; set; }

        public int SourceId { get; set; }

        public string SourceName { get; set; }

        public string SourceType { get; set; }

        public ICollection<string> Tags { get; set; }

        public string TransactionJournalId { get; set; }

        public TransactionType Type { get; set; }

        public string User { get; set; }

        public int? ZoomLevel { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}