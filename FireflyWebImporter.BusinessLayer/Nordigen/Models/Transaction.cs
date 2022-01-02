using System;
using FireflyWebImporter.BusinessLayer.Converters.Enums;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models
{
    public class Transaction
    {
        #region Properties

        public string BankTransactionCode { get; set; }

        public DateTime BookingDate { get; set; }

        public CreditorAccount CreditorAccount { get; set; }

        public string CreditorName { get; set; }

        public CreditorAccount DebtorAccount { get; set; }

        public string DebtorName { get; set; }

        public string EntryReference { get; set; }

        public string RemittanceInformationUnstructured { get; set; }

        public BankType RequisitorBank { get; set; }

        public string RequisitorIban { get; set; }

        public TransactionStatus Status { get; set; }

        public TransactionAmount TransactionAmount { get; set; }

        public string TransactionId { get; set; }

        public DateTime ValueDate { get; set; }

        #endregion
    }
}