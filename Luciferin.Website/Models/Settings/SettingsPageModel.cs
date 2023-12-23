using Luciferin.BusinessLayer.Settings;

namespace Luciferin.Website.Models.Settings
{
    using Classes.Settings;

    public class SettingsPageModel : PageModelBase
    {
        #region Properties

        public PlatformSettings PlatformSettings { get; set; }

        public bool SuccessfullySaved { get; set; }

        public bool ValidationFailed { get; set; }

        #endregion
    }
}