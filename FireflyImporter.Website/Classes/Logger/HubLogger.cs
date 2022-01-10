using System;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Hubs;
using FireflyImporter.BusinessLayer.Logger;
using FireflyImporter.Website.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FireflyImporter.Website.Classes.Logger
{
    public sealed class HubLogger<TCategoryName> : ICompositeLogger<TCategoryName>
    {
        #region Fields

        private readonly IHubContext<ImporterHub, IImporterHub> _importerHub;
        private readonly ILogger<TCategoryName> _logger;

        #endregion

        #region Constructors

        public HubLogger(IHubContext<ImporterHub, IImporterHub> importerHub, ILoggerFactory loggerFactory)
        {
            _importerHub = importerHub;
            _logger = loggerFactory.CreateLogger<TCategoryName>();
        }

        #endregion

        #region Methods

        public async Task Log(LogLevel logLevel, string message)
        {
            _logger.Log(logLevel, message);
            await _importerHub.Clients.All.ImportMessageEvent(DateTime.Now, message);
        }

        /// <inheritdoc />
        public async Task Log(LogLevel logLevel, Exception e, string message)
        {
            _logger.Log(logLevel, e, message);
            await _importerHub.Clients.All.ImportMessageEvent(DateTime.Now, message);
        }

        public async Task LogInformation(string message)
        {
            await Log(LogLevel.Information, message);
        }

        #endregion
    }
}