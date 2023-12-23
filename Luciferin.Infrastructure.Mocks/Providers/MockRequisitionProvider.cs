namespace Luciferin.Infrastructure.Mocks.Providers;

using Application.Abstractions.Providers;
using Core.Entities;

public class MockRequisitionProvider : IRequisitionProvider
{
    public Task<string> AddRequisitionAsync(string name, string institutionId, string returnUrl)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRequisitionAsync(string accountId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Account>> GetAccountsForRequisition(Requisition requisition)
    {
        throw new NotImplementedException();
    }

    public Task<Requisition> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Requisition>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}