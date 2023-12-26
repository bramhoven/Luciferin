namespace Luciferin.Application.Services;

using Abstractions.Providers;
using Abstractions.Services;
using Abstractions.Stores;
using Core.Abstractions;
using Core.Entities.Importable;

public class TransactionImportService : ITransactionImportService
{
    private readonly ICompositeLogger<TransactionImportService> _logger;
    private readonly IImportProvider _provider;
    private readonly ITransactionStore _store;

    public TransactionImportService(ICompositeLogger<TransactionImportService> logger, IImportProvider provider, ITransactionStore store)
    {
        _logger = logger;
        _provider = provider;
        _store = store;
    }

    public Task StartImport(ImportableData importableData)
    {
        throw new NotImplementedException();
    }
}