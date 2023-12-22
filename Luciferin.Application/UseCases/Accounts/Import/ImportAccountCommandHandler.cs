using Luciferin.Application.Abstractions.Providers;
using Luciferin.Application.Abstractions.Stores;
using Luciferin.Core.Abstractions.Services;
using MediatR;

namespace Luciferin.Application.UseCases.Accounts.Import;

public class ImportAccountCommandHandler : IRequestHandler<ImportAccountCommand>
{
    private readonly IAccountFilterService _accountFilterService;
    private readonly IAccountProvider _accountProvider;
    private readonly IAccountStore _accountStore;

    public ImportAccountCommandHandler(IAccountProvider accountProvider, IAccountStore accountStore,
        IAccountFilterService accountFilterService)
    {
        _accountProvider = accountProvider;
        _accountStore = accountStore;
        _accountFilterService = accountFilterService;
    }

    public async Task Handle(ImportAccountCommand request, CancellationToken cancellationToken)
    {
        var providedAccounts = await _accountProvider.GetAllAsync();
        var storedAccounts = await _accountStore.GetAllAsync();
        var filteredAccounts = _accountFilterService.FilterAccounts(providedAccounts, storedAccounts);
        await _accountStore.AddListAsync(filteredAccounts);
    }
}