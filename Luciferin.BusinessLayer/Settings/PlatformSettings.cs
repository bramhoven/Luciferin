using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Settings
{
    public class PlatformSettings
    {
        #region Properties

        public BooleanSetting AutomaticImport { get; set; }

        public BooleanSetting ExtendedNotes { get; set; }

        public StringSetting FireflyAccessToken { get; set; }

        public StringSetting FireflyUrl { get; set; }

        public IntegerSetting ImportDays { get; set; }

        public StringSetting NordigenSecretId { get; set; }

        public StringSetting NordigenSecretKey { get; set; }

        public StringSetting NordigenBaseUrl { get; set; }

        #endregion
    }
}