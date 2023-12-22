using Luciferin.Core.Abstractions.Services;
using Luciferin.Core.Entities;

namespace Luciferin.Core.Services;

public class DuplicateTransactionFilterService : ITransactionFilterService
{
    public ICollection<Transaction> FilterTransactions(ICollection<Transaction> transactions)
    {
        throw new NotImplementedException();
    }
}