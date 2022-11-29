using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Import.Mappers
{
    public class AccountMapper
    {
        public FireflyAccount GetAccount(AccountDetails accountDetails)
        {
            return new FireflyAccount
            {
                Iban = accountDetails.Iban,
                Name = accountDetails.Name,
                Type = AccountType.Asset
            };
        }

        public FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string iban, AccountType[] accountTypes)
        {
            var account = accounts.FirstOrDefault(a => string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) && accountTypes.Contains(a.Type));
            return account ?? new FireflyAccount
            {
                Iban = iban,
                Name = iban,
                Type = accountTypes.FirstOrDefault()
            };
        }

        public FireflyAccount GetAccount(IEnumerable<FireflyAccount> accounts, string name, string iban, AccountType[] accountTypes)
        {
            var fireflyAccounts = accounts
                                  .Where(a => (!string.IsNullOrWhiteSpace(iban) && string.Equals(a.Iban, iban, StringComparison.CurrentCultureIgnoreCase) ||
                                               string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase)) && accountTypes.Contains(a.Type))
                                  .ToList();

            var account = fireflyAccounts.FirstOrDefault(a => a.Type == AccountType.Asset) ?? fireflyAccounts.FirstOrDefault();

            return account ?? new FireflyAccount
            {
                Iban = iban,
                Name = name,
                Type = accountTypes.LastOrDefault()
            };
        }

        private AccountType GetAccountType(string accountIban, ICollection<string> requisitionIbans, string typeName)
        {
            if (!string.IsNullOrWhiteSpace(accountIban) && requisitionIbans.Contains(accountIban))
                return AccountType.Asset;

            return Enum.TryParse(typeName, out AccountType result) ? result : AccountType.Asset;
        }

        public IEnumerable<FireflyAccount> GetAccountsForTransaction(FireflyTransaction transaction, ICollection<string> requisitionIbans)
        {
            yield return new FireflyAccount
            {
                IncludeNetWorth = true,
                Iban = transaction.SourceIban,
                Name = transaction.SourceName,
                Type = GetAccountType(transaction.SourceIban, requisitionIbans, transaction.SourceType)
            };

            yield return new FireflyAccount
            {
                Active = true,
                IncludeNetWorth = true,
                Iban = transaction.DestinationIban,
                Name = transaction.DestinationName,
                Type = GetAccountType(transaction.DestinationIban, requisitionIbans, transaction.DestinationType)
            };
        }
    }
}