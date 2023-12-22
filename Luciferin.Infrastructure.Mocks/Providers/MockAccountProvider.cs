using Luciferin.Application.Abstractions.Providers;
using Luciferin.Core.Entities;

namespace Luciferin.Infrastructure.Mocks.Providers;

public class MockAccountProvider : IAccountProvider
{
    public Task<Account> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> RequestNewAccountConnection(string name, string institutionId, string returnUrl)
    {
        throw new NotImplementedException();
    }
}