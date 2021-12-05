using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;

namespace FireflyWebImporter.BusinessLayer.Firefly.Stores
{
    public interface IFireflyStore
    {
        #region Methods

        Task AddNewTransactions(IEnumerable<FireflyTransaction> transactions);

        Task<ICollection<FireflyAccount>> GetAccounts(AccountType accountType);

        Task<ICollection<FireflyAccount>> GetAccounts();

        Task<ICollection<FireflyTransaction>> GetTransactions();

        #endregion
    }
}