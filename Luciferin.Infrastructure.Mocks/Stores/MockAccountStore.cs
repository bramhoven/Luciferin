using Luciferin.Application.Abstractions.Stores;
using Luciferin.Core.Entities;

namespace Luciferin.Infrastructure.Mocks.Stores;

public class MockAccountStore : IAccountStore
{
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
}