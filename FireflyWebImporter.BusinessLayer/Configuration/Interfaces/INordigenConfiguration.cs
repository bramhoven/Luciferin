namespace FireflyWebImporter.BusinessLayer.Configuration.Interfaces
{
    public interface INordigenConfiguration
    {
        #region Properties

        string NordigenBaseUrl { get; }
        
        string NordigenSecretId { get; }
        
        string NordigenSecretKey { get; }

        #endregion
    }
}