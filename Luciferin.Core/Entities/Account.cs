using Luciferin.Core.Abstractions;
using Luciferin.Core.Enums;

namespace Luciferin.Core.Entities;

public class Account : IEntity
{
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

        public string Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string OpeningBalance { get; set; }

        public DateTime? OpeningBalanceDate { get; set; }

        public AccountType Type { get; set; }
        
        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not Account account)
                return false;

            return string.Equals(Name, account.Name)
                   && string.Equals(Iban, account.Iban)
                   && Type == account.Type;
        }

}