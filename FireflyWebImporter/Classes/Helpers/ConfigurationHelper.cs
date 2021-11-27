using FireflyWebImporter.Classes.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FireflyWebImporter.Classes.Helpers
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Properties

        /// <inheritdoc />
        public string NordigenBaseUrl => _configuration.GetSection(ConfigurationConstants.NordigenBaseUrl).Value;

        /// <inheritdoc />
        public string NordigenSecretId => _configuration.GetSection(ConfigurationConstants.NordigenSecretId).Value;

        /// <inheritdoc />
        public string NordigenSecretKey => _configuration.GetSection(ConfigurationConstants.NordigenSecretKey).Value;

        #endregion

        #region Constructors

        public ConfigurationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion
    }
}