using System;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Hubs;
using FireflyImporter.BusinessLayer.Logger;
using FireflyImporter.Website.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FireflyImporter.Website.Classes.Logger
{
    public sealed class HubLogger : ICompositeLogger
    {
        #region Fields

        private readonly IHubContext<ImporterHub, IImporterHub> _importerHub;
        private ILogger _logger;

        #endregion

        #region Constructors

        public HubLogger(IHubContext<ImporterHub, IImporterHub> importerHub)
        {
            _importerHub = importerHub;
        }

        #endregion

        #region Methods

        public async Task Log(LogLevel logLevel, string message)
        {
            _logger.Log(logLevel, message);
            await _importerHub.Clients.All.ImportMessageEvent(DateTime.Now, message);
        }

        public async Task LogInformation(string message)
        {
            await Log(LogLevel.Information, message);
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        #endregion
    }
}