using System;
using FireflyImporter.BusinessLayer.Firefly.Enums;

namespace FireflyImporter.BusinessLayer.Firefly.Models
{
    public class FireflyAccount
    {
        #region Properties

        public string AccountNumber { get; set; }

        public string AccountRole { get; set; }

        public bool Active { get; set; }

        public string Bic { get; set; }

        public string CreditCardType { get; set; }

        public string CurrencyCode { get; set; }

        public int CurrencyDecimalPlaces { get; set; }

        public string CurrencyId { get; set; }

        public string CurrencySymbol { get; set; }

        public string CurrentBalance { get; set; }

        public DateTime? CurrentBalanceDate { get; set; }

        public string CurrentDebt { get; set; }

        public string Iban { get; set; }

        public int Id { get; set; }

        public bool IncludeNetWorth { get; set; }

        public string Interest { get; set; }

        public string InterestPeriod { get; set; }

        public double? Latitude { get; set; }

        public string LiabilityDirection { get; set; }

        public string LiabilityType { get; set; }

        public double? Longitude { get; set; }

        public DateTime? MonthlyPaymentDate { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string OpeningBalance { get; set; }

        public DateTime? OpeningBalanceDate { get; set; }

        public int Order { get; set; }

        public AccountType Type { get; set; }

        public string VirtualBalance { get; set; }

        public int? ZoomLevel { get; set; }

        #endregion
    }
}