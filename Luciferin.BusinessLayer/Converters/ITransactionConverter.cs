using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Converters
{
    public interface ITransactionConverter
    {
        #region Methods

        /// <summary>
        /// Converts the Nordigen transaction to a Firefly transaction.
        /// </summary>
        /// <param name="transaction">The Nordigen transaction.</param>
        /// <param name="tag">The import tag to add.</param>
        /// <returns></returns>
        FireflyTransaction ConvertTransaction(Transaction transaction, string tag);
        
        #endregion
    }
}