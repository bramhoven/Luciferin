using Luciferin.Core.Abstractions.Services;
using Luciferin.Core.Entities;

namespace Luciferin.Infrastructure.Mocks.Services;

public class MockAccountFilterService : IAccountFilterService
{
    public ICollection<Account> FilterAccounts(ICollection<Account> accounts, ICollection<Account> existingAccounts)
    {
        return accounts.Where(a => !existingAccounts.Contains(a)).ToList();
    }
}