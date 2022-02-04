using Luciferin.BusinessLayer.Settings;

namespace Luciferin.Website.Models.Settings
{
    public class SettingsIndexPageModel : PageModelBase
    {
        #region Properties

        public PlatformSettings Settings { get; set; }

        #endregion
    }
}