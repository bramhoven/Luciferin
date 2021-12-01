using System.Collections.Generic;
using System.Threading.Tasks;
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
        public Task<ICollection<FireflyTransaction>> GetTransactions()
        {
            return _store.GetTransactions();
        }

        /// <inheritdoc />
        public async Task Test() { }

        #endregion
    }
}