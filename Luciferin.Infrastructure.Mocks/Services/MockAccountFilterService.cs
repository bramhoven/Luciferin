using Luciferin.Core.Abstractions.Services;
using Luciferin.Core.Entities;

namespace Luciferin.Infrastructure.Mocks.Services;

public class MockAccountFilterService : IAccountFilterService
{
    public ICollection<Account> FilterAccounts(ICollection<Requisition> accounts, ICollection<Account> existingAccounts)
    {
        return existingAccounts;
    }
}