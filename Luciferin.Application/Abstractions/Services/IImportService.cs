namespace Luciferin.Application.Abstractions.Services;

public interface IImportService
{
    Task StartImport(int historicalDays);
}