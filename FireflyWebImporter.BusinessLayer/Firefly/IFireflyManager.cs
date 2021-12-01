using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly.Models;

namespace FireflyWebImporter.BusinessLayer.Firefly
{
    public interface IFireflyManager
    {
        #region Methods

        Task<ICollection<FireflyTransaction>> GetTransactions();

        Task Test();

        #endregion
    }
}