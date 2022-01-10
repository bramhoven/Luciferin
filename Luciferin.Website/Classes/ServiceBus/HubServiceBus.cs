using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Hubs;
using Luciferin.BusinessLayer.ServiceBus;
using Luciferin.Website.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Luciferin.Website.Classes.ServiceBus
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