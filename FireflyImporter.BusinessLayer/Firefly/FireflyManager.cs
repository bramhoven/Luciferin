﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Firefly.Enums;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Firefly.Stores;

namespace FireflyImporter.BusinessLayer.Firefly
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
        public Task<ICollection<FireflyTransaction>> GetTransactions()
        {
            return _store.GetTransactions();
        }

        /// <inheritdoc />
        public Task UpdateAccount(FireflyAccount fireflyAccount)
        {
            return _store.UpdateAccount(fireflyAccount);
        }

        #endregion
    }
}