using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Enums;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Firefly.Stores;

namespace Luciferin.BusinessLayer.Firefly
{
    public class FireflyManager : IFireflyManager
    {
        #region Fields

        private readonly IFireflyStore _store;

        #endregion

        #region Constructors

        public FireflyManager(IFireflyStore fireflyStore)
        {
            _store = fireflyStore;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public Task AddNewAccounts(ICollection<FireflyAccount> accounts)
        {
            return _store.AddNewAccounts(accounts);
        }

        /// <inheritdoc />
        public Task AddNewTag(FireflyTag tag)
        {
            return _store.AddNewTag(tag);
        }

        /// <inheritdoc />
        public Task AddNewTransactions(IEnumerable<FireflyTransaction> transactions)
        {
            return _store.AddNewTransactions(transactions);
        }

        /// <inheritdoc />
        public Task<ICollection<FireflyAccount>> GetAccounts(AccountType accountType)
        {
            return _store.GetAccounts(accountType);
        }

        /// <inheritdoc />
        public Task<ICollection<FireflyAccount>> GetAccounts()
        {
            return _store.GetAccounts();
        }

        /// <inheritdoc />
        public Task<FireflyTransaction> GetFirstTransactionOfAccount(int accountId)
        {
            return _store.GetFirstTransactionOfAccount(accountId);
        }

        /// <inheritdoc />
        public Task<ICollection<FireflyTransaction>> GetTransactions(DateTime fromDate)
        {
            return _store.GetTransactions(fromDate);
        }

        /// <inheritdoc />
        public Task UpdateAccount(FireflyAccount fireflyAccount)
        {
            return _store.UpdateAccount(fireflyAccount);
        }

        #endregion
    }
}