namespace Luciferin.BusinessLayer.Configuration.Interfaces
{
    public interface IImportConfiguration
    {
        #region Properties

        string StorageConnectionString { get; }

        string StorageProvider { get; }

        #endregion
    }
}