namespace Luciferin.Application.Abstractions.Providers;

using Core.Entities;
using Core.Entities.Importable;

public interface IImportProvider
{
    /// <summary>
    ///     Gets a list of importable requisitions and their accounts.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<Requisition>> GetImportableRequisitions();

    /// <summary>
    ///     Get the importable data for a specific date.
    /// </summary>
    /// <param name="dateTime">The date for which to get the importable data.</param>
    /// <returns></returns>
    Task<ImportableData> GetImportableData(DateTime dateTime);
}