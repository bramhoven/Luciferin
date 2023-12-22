using Luciferin.Core.Entities;

namespace Luciferin.Application.Abstractions.Providers;

public interface IAccountProvider : IGenericProvider<Account>
{
    /// <summary>
    ///     Request a new account connection.
    /// </summary>
    /// <param name="name">The name of the new connection.</param>
    /// <param name="institutionId">The id of the institution where the account lives.</param>
    /// <param name="returnUrl">The return url where the user will be redirected to when going back.</param>
    /// <returns></returns>
    Task<string> RequestNewAccountConnection(string name, string institutionId, string returnUrl);
}