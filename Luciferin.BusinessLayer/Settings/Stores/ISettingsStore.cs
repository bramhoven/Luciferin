using System.Collections.Generic;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Settings.Stores
{
    public interface ISettingsStore
    {
        #region Methods

        /// <summary>
        /// Gets a setting by it's name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns></returns>
        ISetting GetSetting(string name);

        /// <summary>
        /// Gets all the settings.
        /// </summary>
        /// <returns></returns>
        ICollection<ISetting> GetSettings();

        /// <summary>
        /// Updates the value of a setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        void UpdateSetting(ISetting setting);

        #endregion
    }
}