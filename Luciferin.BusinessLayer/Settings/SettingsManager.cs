using System;
using System.Collections.Generic;
using Luciferin.BusinessLayer.Settings.Mappers;
using Luciferin.BusinessLayer.Settings.Models;
using Luciferin.BusinessLayer.Settings.Stores;
using NLog;

namespace Luciferin.BusinessLayer.Settings
{
    public class SettingsManager : ISettingsManager
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

        /// <inheritdoc />
        public PlatformSettings GetPlatformSettings()
        {
            var settings = GetSettings();
            return PlatformSettingsMapper.MapSettingsCollectionToPlatformSettings(settings);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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