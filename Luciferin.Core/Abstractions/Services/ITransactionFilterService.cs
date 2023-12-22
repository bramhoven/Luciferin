using Luciferin.Core.Entities;

namespace Luciferin.Core.Abstractions.Services;

public interface ITransactionFilterService
{
    /// <summary>
    ///     Filters a list of transactions.
    /// </summary>
    /// <param name="transactions">The transactions to filter.</param>
    /// <returns></returns>
    ICollection<Transaction> FilterTransactions(ICollection<Transaction> transactions);
}