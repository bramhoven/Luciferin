﻿using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Firefly.Models;
using Luciferin.BusinessLayer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Luciferin.Website.Hubs
{
    public sealed class ImporterHub : Hub<IImporterHub>
    {
        #region Methods

        public async Task ImportMessageEvent(DateTime time, string message)
        {
            await Clients.All.ImportMessageEvent(time, message);
        }

        public async Task ImportTransactionEvent(FireflyTransaction transaction, bool successful, string fireflyUrl)
        {
            await Clients.All.ImportTransactionEvent(transaction, successful, fireflyUrl);
        }

        #endregion
    }
}