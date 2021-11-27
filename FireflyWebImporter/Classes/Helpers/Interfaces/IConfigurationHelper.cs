namespace FireflyWebImporter.Classes.Helpers.Interfaces
{
    public interface IConfigurationHelper
    {
        #region Properties

        string NordigenBaseUrl { get; }
        string NordigenSecretId { get; }
        string NordigenSecretKey { get; }

        #endregion
    }
}