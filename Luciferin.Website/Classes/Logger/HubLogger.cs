using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Hubs;
using Luciferin.BusinessLayer.Logger;
using Luciferin.Website.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Luciferin.Website.Classes.Logger
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

        /// <inheritdoc />
        public Task LogDebug(string message)
        {
            return Log(LogLevel.Debug, message);
        }

        /// <inheritdoc />
        public Task LogError(string message)
        {
            return Log(LogLevel.Error, message);
        }

        /// <inheritdoc />
        public Task LogInformation(string message)
        {
            return Log(LogLevel.Information, message);
        }

        /// <inheritdoc />
        public Task LogWarning(string message)
        {
            return Log(LogLevel.Warning, message);
        }

        #endregion
    }
}