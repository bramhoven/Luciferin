namespace Luciferin.Application.Abstractions.Providers;

using Core.Entities;

public interface IRequisitionProvider : IGenericProvider<Requisition>
{
    /// <summary>
    ///     Request a new import account connection.
    /// </summary>
    /// <param name="name">The name of the new connection.</param>
    /// <param name="institutionId">The id of the institution where the account lives.</param>
    /// <param name="returnUrl">The return url where the user will be redirected to when going back.</param>
    /// <returns></returns>
    Task<string> AddRequisitionAsync(string name, string institutionId, string returnUrl);

    /// <summary>
    ///     Deletes an import account.
    /// </summary>
    /// <param name="accountId">The id of the account.</param>
    /// <returns></returns>
    Task<bool> DeleteRequisitionAsync(string accountId);

    /// <summary>
    ///     Gets a list of the accounts for the requisition.
    /// </summary>
    /// <param name="requisition">The requisition for which to get the accounts.</param>
    /// <returns></returns>
    Task<ICollection<Account>> GetAccountsForRequisition(Requisition requisition);
}