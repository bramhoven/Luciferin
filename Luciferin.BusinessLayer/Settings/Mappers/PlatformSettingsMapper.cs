using System.Collections.Generic;
using System.Linq;
using Luciferin.BusinessLayer.Settings.Extentions;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Settings.Mappers
{
    public static class PlatformSettingsMapper
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Maps a collection of ISetting to the PlatformSettings model.
        /// </summary>
        /// <param name="settings">The settings collection.</param>
        /// <returns></returns>
        public static PlatformSettings MapSettingsCollectionToPlatformSettings(ICollection<ISetting> settings)
        {
            return new PlatformSettings
            {
                FireflyUrl = settings.GetSetting<StringSetting>(SettingKeyConstants.FireflyUrlKey),
                FireflyAccessToken = settings.GetSetting<StringSetting>(SettingKeyConstants.FireflyAccessTokenKey),
                NordigenBaseUrl = settings.GetSetting<StringSetting>(SettingKeyConstants.NordigenBaseUrlKey),
                NordigenSecretId = settings.GetSetting<StringSetting>(SettingKeyConstants.NordigenSecretIdKey),
                NordigenSecretKey = settings.GetSetting<StringSetting>(SettingKeyConstants.NordigenSecretKeyKey),
                ImportDays = settings.GetSetting<IntegerSetting>(SettingKeyConstants.ImportDaysKey),
                ExtendedNotes = settings.GetSetting<BooleanSetting>(SettingKeyConstants.ExtendedNotesKey),
                AutomaticImport = settings.GetSetting<BooleanSetting>(SettingKeyConstants.AutomaticImportKey)
            };
        }

        #endregion

        #endregion
    }
}