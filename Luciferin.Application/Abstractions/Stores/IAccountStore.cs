namespace Luciferin.Application.Abstractions.Stores;

using Core.Entities;

public interface IAccountStore : IGenericStore<Account>
{
    /// <summary>
    ///     Gets the new asset accounts.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<Account>> GetNewAssetAccounts();
}