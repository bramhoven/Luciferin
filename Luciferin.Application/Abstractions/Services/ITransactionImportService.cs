namespace Luciferin.Application.Abstractions.Services;

using Core.Entities.Importable;

public interface ITransactionImportService
{
    Task StartImport(ImportableData importableData);
}