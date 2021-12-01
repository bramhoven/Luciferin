namespace FireflyWebImporter.Classes.Helpers.Interfaces
{
    public interface IConfigurationHelper
    {
        #region Properties

        string FireflyBaseUrl { get; }
        string FireflyAccessToken { get; }
        string NordigenBaseUrl { get; }
        string NordigenSecretId { get; }
        string NordigenSecretKey { get; }

        #endregion
    }
}