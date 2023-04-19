using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Models;

namespace Luciferin.BusinessLayer.Firefly
{
    public interface IFireflyManager
    {
        #region Methods

        Task AddNewAccounts(ICollection<FireflyAccount> accounts);

        Task AddNewTag(FireflyTag tag);

        Task AddNewTransactions(IEnumerable<FireflyTransaction> transactions);

        Task<ICollection<FireflyAccount>> GetAccounts(AccountType accountType);

        Task<ICollection<FireflyAccount>> GetAccounts();

        Task<FireflyTransaction> GetFirstTransactionOfAccount(int accountId);

        Task<ICollection<FireflyTransaction>> GetTransactions(DateTime fromDate);

        Task UpdateAccount(FireflyAccount fireflyAccount);

        #endregion
    }
}