using System.Collections.Generic;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Settings
{
    public interface ISettingsManager
    {
        #region Methods

        /// <summary>
        /// Gets the settings in the platform settings model.
        /// </summary>
        /// <returns></returns>
        PlatformSettings GetPlatformSettings();

        /// <summary>
        /// Gets a setting by it's name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns></returns>
        ISetting GetSetting(string name);

        /// <summary>
        /// Gets a list of all the available settings.
        /// </summary>
        /// <returns></returns>
        ICollection<ISetting> GetSettings();

        /// <summary>
        /// Sets a specific setting.
        /// </summary>
        /// <param name="setting">The setting to update.</param>
        /// <returns></returns>
        bool UpdateSetting(ISetting setting);

        #endregion
    }
}