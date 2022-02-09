using Luciferin.BusinessLayer.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Luciferin.BusinessLayer.Configuration
{
    public class Configuration : ICompositeConfiguration
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Properties

        /// <inheritdoc />
        public string StorageConnectionString => _configuration.GetSection(ImportConfigurationConstants.StorageConnectionString).Value;

        /// <inheritdoc />
        public string StorageProvider => !string.IsNullOrWhiteSpace(_configuration.GetSection(ImportConfigurationConstants.StorageProvider).Value) ? _configuration.GetSection(ImportConfigurationConstants.StorageProvider).Value.ToLower() : "mysql";

        #endregion

        #region Constructors

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion
    }
}