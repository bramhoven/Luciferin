using Luciferin.Core.Entities;

namespace Luciferin.Core.Abstractions.Services;

public interface IAccountFilterService
{
    /// <summary>
    /// Filters a list of accounts.
    /// </summary>
    /// <param name="accounts">The accounts to filter.</param>
    /// <param name="existingAccounts">The accounts to filter against.</param>
    /// <returns></returns>
    ICollection<Account> FilterAccounts(ICollection<Account> accounts, ICollection<Account> existingAccounts);
}