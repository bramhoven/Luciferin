namespace Luciferin.Application.Abstractions.Services;

using Core.Entities;
using Core.Entities.Importable;

public interface IAccountClassifierService
{
    /// <summary>
    ///     Get a list of all the importable account and classify them.
    ///     This also classifies "hidden" saving accounts that are not directly importable
    /// </summary>
    /// <param name="importableData">The data from which to get the accounts.</param>
    /// <returns></returns>
    Task<ICollection<Account>> GetAndClassifyImportableAccounts(ImportableData importableData);
}