using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly.Models;

namespace FireflyWebImporter.BusinessLayer.Firefly.Stores
{
    public interface IFireflyStore
    {
        #region Methods

        Task<ICollection<FireflyTransaction>> GetTransactions();

        #endregion
    }
}