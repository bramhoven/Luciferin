namespace Luciferin.Application.Abstractions.Services;

using Core.Entities;

public interface ITransactionFilterService
{
    /// <summary>
    ///     Filters a list of transactions.
    /// </summary>
    /// <param name="transactions">The transactions to filter.</param>
    /// <returns></returns>
    ICollection<Transaction> FilterTransactions(ICollection<Transaction> transactions);
}