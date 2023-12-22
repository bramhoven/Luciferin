using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Hubs;
using Luciferin.BusinessLayer.Logger;
using Luciferin.Website.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Luciferin.Website.Classes.Logger
{
    public sealed class CoreHubLogger<TCategoryName> : Core.Abstractions.ICompositeLogger<TCategoryName>
    {
        #region Fields

        private readonly IHubContext<ImporterHub, IImporterHub> _importerHub;
        private readonly ILogger<TCategoryName> _logger;

        #endregion

        #region Constructors

        public CoreHubLogger(IHubContext<ImporterHub, IImporterHub> importerHub, ILoggerFactory loggerFactory)
        {
            _importerHub = importerHub;
            _logger = loggerFactory.CreateLogger<TCategoryName>();
        }

        #endregion

        #region Methods

        private LogLevel MapLogLevel(Core.Enums.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Core.Enums.LogLevel.Information:
                    return LogLevel.Information;
                
                case Core.Enums.LogLevel.Debug:
                    return LogLevel.Debug;
                
                case Core.Enums.LogLevel.Warning:
                    return LogLevel.Warning;
                
                case Core.Enums.LogLevel.Error:
                    return LogLevel.Error;
                
                case Core.Enums.LogLevel.Critical:
                    return LogLevel.Critical;
                
                case Core.Enums.LogLevel.Trace:
                    return LogLevel.Trace;
                
                case Core.Enums.LogLevel.None:
                    return LogLevel.None;
            }

            return LogLevel.None;
        }

        public async Task Log(Core.Enums.LogLevel logLevel, string message)
        {
            _logger.Log(MapLogLevel(logLevel), message);
            await _importerHub.Clients.All.ImportMessageEvent(DateTime.Now, message);
        }

        /// <inheritdoc />
        public async Task Log(Core.Enums.LogLevel logLevel, Exception e, string message)
        {
            _logger.Log(MapLogLevel(logLevel), e, message);
            await _importerHub.Clients.All.ImportMessageEvent(DateTime.Now, message);
        }

        /// <inheritdoc />
        public Task LogDebug(string message)
        {
            return Log(Core.Enums.LogLevel.Debug, message);
        }

        /// <inheritdoc />
        public Task LogError(string message)
        {
            return Log(Core.Enums.LogLevel.Error, message);
        }

        /// <inheritdoc />
        public Task LogInformation(string message)
        {
            return Log(Core.Enums.LogLevel.Information, message);
        }

        /// <inheritdoc />
        public Task LogWarning(string message)
        {
            return Log(Core.Enums.LogLevel.Warning, message);
        }

        #endregion
    }
}