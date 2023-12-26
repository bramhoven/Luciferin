namespace Luciferin.Infrastructure.GoCardless.Abstractions;

using Core.Entities;
using Models;

public interface ITransactionConverter
{
    #region Methods

    /// <summary>
    ///     Converts the GoCardless transaction to a transaction.
    /// </summary>
    /// <param name="gcTransaction">The GoCardless transaction.</param>
    /// <param name="tag">The import tag to add.</param>
    /// <returns></returns>
    Transaction ConvertTransaction(GCTransaction gcTransaction, string tag);

    #endregion
}