using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.BusinessLayer.Converters
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