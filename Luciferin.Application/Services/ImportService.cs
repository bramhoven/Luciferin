namespace Luciferin.Application.Services;

using Abstractions.Providers;
using Abstractions.Services;
using Abstractions.Stores;

public class ImportService : IImportService
{
    private readonly IAccountClassifierService _accountClassifierService;
    private readonly IAccountService _accountService;
    private readonly IAccountStore _accountStore;
    private readonly IImportProvider _importProvider;
    private readonly ITransactionImportService _transactionImportService;

    public ImportService(IImportProvider importProvider, IAccountStore accountStore, IAccountClassifierService accountClassifierService, ITransactionImportService transactionImportService, IAccountService accountService)
    {
        _importProvider = importProvider;
        _accountStore = accountStore;
        _accountClassifierService = accountClassifierService;
        _transactionImportService = transactionImportService;
        _accountService = accountService;
    }

    /// <inheritdoc />
    public async Task StartImport(int historicalDays)
    {
        // Loop through all the historical days and import day by day
        var startDate = DateTime.Today.AddDays(-historicalDays);
        for (var day = 0; day <= historicalDays; day++)
        {
            var curDate = startDate.AddDays(day);
            var importableData = await _importProvider.GetImportableData(curDate);

            // Classify account to check which are assets
            var importableAccounts = await _accountClassifierService.GetAndClassifyImportableAccounts(importableData);
            await _accountStore.AddListAsync(importableAccounts);

            // Start transaction import
            await _transactionImportService.StartImport(importableData);
        }

        var newAssetAccounts = await _accountStore.GetNewAssetAccounts();
        await _accountService.RecalculateAccountBalances(newAssetAccounts);
    }
}