using System;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Firefly.Models;
using FireflyImporter.BusinessLayer.Hubs;
using FireflyImporter.BusinessLayer.ServiceBus;
using FireflyImporter.Website.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FireflyImporter.Website.Classes.ServiceBus
{
    public sealed class HubServiceBus : IServiceBus
    {
        #region Fields

        private readonly IHubContext<ImporterHub, IImporterHub> _importerHub;

        #endregion

        #region Constructors

        public HubServiceBus(IHubContext<ImporterHub, IImporterHub> importerHub)
        {
            _importerHub = importerHub;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public Task PublishTransactionEvent(FireflyTransaction transaction, bool successful)
        {
            return _importerHub.Clients.All.ImportTransactionEvent(transaction, successful);
        }

        #endregion
    }
}