namespace Luciferin.Application.Abstractions.Services;

using Core.Entities;

public interface IAccountService
{
    /// <summary>
    ///     Recalculates the account balances for a list of accounts.
    /// </summary>
    /// <returns></returns>
    Task RecalculateAccountBalances(ICollection<Account> accounts);
}