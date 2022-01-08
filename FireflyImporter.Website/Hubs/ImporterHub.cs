using System;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FireflyImporter.Website.Hubs
{
    public sealed class ImporterHub : Hub<IImporterHub>
    {
        #region Methods

        public async Task ImportMessageEvent(DateTime time, string message)
        {
            await Clients.All.ImportMessageEvent(time, message);
        }

        /// <inheritdoc />
        public async Task ImportTransactionEvent(object transactionEventModel)
        {
            await Clients.All.ImportTransactionEvent(transactionEventModel);
        }

        #endregion
    }
}