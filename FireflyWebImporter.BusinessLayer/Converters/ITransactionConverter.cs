using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Nordigen.Models;

namespace FireflyWebImporter.BusinessLayer.Converters
{
    public interface ITransactionConverter
    {
        #region Methods

        /// <summary>
        /// Converts the Nordigen transaction to a Firefly transaction.
        /// </summary>
        /// <param name="transaction">The Nordigen transaction.</param>
        /// <returns></returns>
        FireflyTransaction ConvertTransaction(Transaction transaction);
        
        #endregion
    }
}