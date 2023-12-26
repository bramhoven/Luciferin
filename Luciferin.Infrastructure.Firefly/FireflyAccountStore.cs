using Luciferin.Application.Abstractions;
using Luciferin.Application.Abstractions.Stores;
using Luciferin.Core.Abstractions;
using Luciferin.Core.Entities;
using Microsoft.Extensions.Options;

namespace Luciferin.Infrastructure.Firefly;

using Settings;

public class FireflyAccountStore : IAccountStore
{
    private readonly string _fireflyAccessToken;

    private readonly string _fireflyBaseUrl;

    private readonly ICompositeLogger<FireflyAccountStore> _logger;

    private readonly IServiceBus _serviceBus;

    public FireflyAccountStore(IOptions<FireflySettings> options, ICompositeLogger<FireflyAccountStore> logger, IServiceBus serviceBus)
    {
        var fireflySettings = options.Value;
        _fireflyBaseUrl = fireflySettings.BaseUrl;
        _fireflyAccessToken = fireflySettings.AccessToken;
        _logger = logger;
        _serviceBus = serviceBus;
    }

    public IQueryable<Account> Entities { get; }

    public Task<Account> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Account> AddAsync(Account entity)
    {
        throw new NotImplementedException();
    }

    public Task<Account> AddListAsync(IEnumerable<Account> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Account entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Account entity)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Account>> GetNewAssetAccounts()
    {
        throw new NotImplementedException();
    }
}