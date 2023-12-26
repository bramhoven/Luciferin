namespace Luciferin.Application.Services;

using Abstractions.Services;
using Core.Entities;

public class DuplicateTransactionFilterService : ITransactionFilterService
{
    public ICollection<Transaction> FilterTransactions(ICollection<Transaction> transactions)
    {
        throw new NotImplementedException();
    }
}