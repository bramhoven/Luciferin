using System;
using Luciferin.BusinessLayer.Firefly.Enums;

namespace Luciferin.BusinessLayer.Firefly.Models
{
    public class FireflyAccount
    {
        public FireflyAccount()
        {
            AccountNumber = string.Empty;
            AccountRole = "defaultAsset";
            Active = true;
            Bic = string.Empty;
            CreditCardType = string.Empty;
            CurrencyCode = "EUR";
            CurrencyId = "1";
            CurrencySymbol = string.Empty;
            CurrentBalance = "0";
            CurrentDebt = "0";
            Iban = string.Empty;
            Interest = "0";
            InterestPeriod = string.Empty;
            LiabilityDirection = string.Empty;
            LiabilityType = string.Empty;
            Name = string.Empty;
            Notes = string.Empty;
            OpeningBalance = string.Empty;
            Type = AccountType.Asset;
            VirtualBalance = string.Empty;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not FireflyAccount fireflyAccount)
                return false;

            return Name.Equals(fireflyAccount.Name) && Iban.Equals(fireflyAccount.Iban) && Type == fireflyAccount.Type;
        }

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