using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;

namespace FireflyWebImporter.BusinessLayer.Firefly
{
    public interface IFireflyManager
    {
        #region Methods

        Task AddNewTransactions(IEnumerable<FireflyTransaction> transactions);

        Task<ICollection<FireflyAccount>> GetAccounts(AccountType accountType);

        Task<ICollection<FireflyAccount>> GetAccounts();

        Task<FireflyTransaction> GetFirstTransactionOfAccount(int accountId);

        Task<ICollection<FireflyTransaction>> GetTransactions();

        Task UpdateAccount(FireflyAccount fireflyAccount);

        #endregion
    }
}