using Luciferin.BusinessLayer.Settings;

namespace Luciferin.Website.Models.Settings
{
    public class SettingsPageModel : PageModelBase
    {
        #region Properties

        public PlatformSettings Settings { get; set; }

        public bool SuccessfullySaved { get; set; }

        public bool ValidationFailed { get; set; }

        #endregion
    }
}