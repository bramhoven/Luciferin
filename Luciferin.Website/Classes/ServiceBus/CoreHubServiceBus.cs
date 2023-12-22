using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Hubs;
using Luciferin.BusinessLayer.ServiceBus;
using Luciferin.Core.Entities;
using Luciferin.Website.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Luciferin.Website.Classes.ServiceBus
{
    public sealed class CoreHubServiceBus : Application.Abstractions.IServiceBus
    {
        #region Fields

        private readonly IHubContext<ImporterHub, IImporterHub> _importerHub;

        #endregion

        #region Constructors

        public CoreHubServiceBus(IHubContext<ImporterHub, IImporterHub> importerHub)
        {
            _importerHub = importerHub;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public Task PublishTransactionEvent(Transaction transaction, bool successful, string fireflyUrl)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}