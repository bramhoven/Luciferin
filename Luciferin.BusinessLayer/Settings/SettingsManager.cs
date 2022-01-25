using System;
using System.Collections.Generic;
using Luciferin.BusinessLayer.Settings.Models;
using Luciferin.BusinessLayer.Settings.Stores;
using NLog;

namespace Luciferin.BusinessLayer.Settings
{
    public class SettingsManager
    {
        #region Fields

        private readonly ISettingsStore _settingsStore;

        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public SettingsManager(ISettingsStore settingsStore)
        {
            _settingsStore = settingsStore;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a setting by it's name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns></returns>
        public ISetting GetSetting(string name)
        {
            try
            {
                return _settingsStore.GetSetting(name);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return null;
        }

        /// <summary>
        /// Gets a list of all the available settings.
        /// </summary>
        /// <returns></returns>
        public ICollection<ISetting> GetSettings()
        {
            try
            {
                return _settingsStore.GetSettings();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return new List<ISetting>();
        }

        /// <summary>
        /// Sets a specific setting.
        /// </summary>
        /// <param name="setting">The setting to update.</param>
        /// <returns></returns>
        public bool UpdateSetting(ISetting setting)
        {
            try
            {
                _settingsStore.UpdateSetting(setting);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }

        #endregion
    }
}