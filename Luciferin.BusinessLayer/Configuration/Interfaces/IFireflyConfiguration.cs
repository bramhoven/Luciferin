namespace Luciferin.BusinessLayer.Configuration.Interfaces
{
    public interface IFireflyConfiguration
    {
        #region Properties

        string FireflyAccessToken { get; }

        string FireflyBaseUrl { get; }

        #endregion
    }
}