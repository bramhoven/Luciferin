namespace Luciferin.Application.Abstractions.Providers;

using Core.Entities;

public interface ITransactionProvider
{
    /// <summary>
    ///     Gets all the transactions for an account between to dates.
    /// </summary>
    /// <param name="account">The account.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns></returns>
    Task<ICollection<Transaction>> GetTransactionsForAccount(Account account, DateTime startDate, DateTime endDate);
}