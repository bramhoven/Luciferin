namespace Luciferin.Application.UseCases.Accounts.Import;

using Abstractions.Providers;
using Abstractions.Stores;
using Core.Abstractions.Services;
using MediatR;

public class ImportAccountCommandHandler : IRequestHandler<ImportAccountCommand>
{
    private readonly IAccountFilterService _accountFilterService;
    private readonly IAccountStore _accountStore;
    private readonly IRequisitionProvider _requisitionProvider;

    public ImportAccountCommandHandler(IRequisitionProvider requisitionProvider, IAccountStore accountStore, IAccountFilterService accountFilterService)
    {
        _requisitionProvider = requisitionProvider;
        _accountStore = accountStore;
        _accountFilterService = accountFilterService;
    }

    public async Task Handle(ImportAccountCommand request, CancellationToken cancellationToken)
    {
        /*
         var providedAccounts = await _importAccountProvider.GetAllAsync();
        var storedAccounts = await _accountStore.GetAllAsync();
        var filteredAccounts = _accountFilterService.FilterAccounts(providedAccounts, storedAccounts);
        await _accountStore.AddListAsync(filteredAccounts);
        */
    }
}