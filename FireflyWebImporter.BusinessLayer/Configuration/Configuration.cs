using FireflyWebImporter.BusinessLayer.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FireflyWebImporter.BusinessLayer.Configuration
{
    public class Configuration : ICompositeConfiguration
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Properties

        /// <inheritdoc />
        public int DaysToSync => int.Parse(_configuration.GetSection(ImportConfigurationConstants.DaysToSync).Value);

        /// <inheritdoc />
        public string FireflyAccessToken => _configuration.GetSection(FireflyConfigurationConstants.FireflyAccessToken).Value;

        /// <inheritdoc />
        public string FireflyBaseUrl => _configuration.GetSection(FireflyConfigurationConstants.FireflyBaseUrl).Value;

        /// <inheritdoc />
        public string NordigenBaseUrl => _configuration.GetSection(NordigenConfigurationConstants.NordigenBaseUrl).Value;

        /// <inheritdoc />
        public string NordigenSecretId => _configuration.GetSection(NordigenConfigurationConstants.NordigenSecretId).Value;

        /// <inheritdoc />
        public string NordigenSecretKey => _configuration.GetSection(NordigenConfigurationConstants.NordigenSecretKey).Value;

        #endregion

        #region Constructors

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion
    }
}