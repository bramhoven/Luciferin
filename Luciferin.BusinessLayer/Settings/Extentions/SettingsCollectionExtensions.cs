using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.BusinessLayer.Exceptions;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Settings.Extentions
{
    public static class SettingsCollectionExtensions
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Gets the typed setting from a settings collection.
        /// </summary>
        /// <param name="settings">The settings collection.</param>
        /// <param name="key">The setting key.</param>
        /// <typeparam name="TSettingType">The setting type.</typeparam>
        /// <returns></returns>
        public static TSettingType GetSetting<TSettingType>(this IEnumerable<ISetting> settings, string key)
        {
            var setting = settings.FirstOrDefault(s => string.Equals(s.Key, key, StringComparison.InvariantCultureIgnoreCase));
            if (setting == null)
                throw new SettingNotFoundException($"Setting not found with key: {key}");

            return (TSettingType)setting;
        }

        #endregion

        #endregion
    }
}