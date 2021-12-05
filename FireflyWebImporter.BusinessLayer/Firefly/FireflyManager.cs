using System.Collections.Generic;
using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Firefly.Enums;
using FireflyWebImporter.BusinessLayer.Firefly.Models;
using FireflyWebImporter.BusinessLayer.Firefly.Stores;

namespace FireflyWebImporter.BusinessLayer.Firefly
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
        public Task<ICollection<FireflyTransaction>> GetTransactions()
        {
            return _store.GetTransactions();
        }

        /// <inheritdoc />
        public async Task Test() { }

        #endregion
    }
}