using System;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenTransaction
    {
        #region Properties

        [JsonProperty("bankTransactionCode")]
        public string BankTransactionCode { get; set; }

        [JsonProperty("bookingDate")]
        public DateTime BookingDate { get; set; }

        [JsonProperty("creditorAccount")]
        public NordigenCreditorAccount CreditorAccount { get; set; }

        [JsonProperty("creditorName")]
        public string CreditorName { get; set; }

        [JsonProperty("debtorAccount")]
        public NordigenCreditorAccount DebtorAccount { get; set; }

        [JsonProperty("DebtorName")]
        public string DebtorName { get; set; }

        [JsonProperty("entryReference")]
        public string EntryReference { get; set; }

        [JsonProperty("remittanceInformationUnstructured")]
        public string RemittanceInformationUnstructured { get; set; }

        [JsonProperty("transactionAmount")]
        public TransactionAmount TransactionAmount { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("valueDate")]
        public DateTime ValueDate { get; set; }

        #endregion
    }
}