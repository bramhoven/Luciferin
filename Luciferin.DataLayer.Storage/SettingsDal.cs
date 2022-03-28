using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.DataLayer.Storage.Context;
using Luciferin.DataLayer.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.DataLayer.Storage
{
    public class SettingsDal : BaseDal
    {
        #region Constructors

        /// <inheritdoc />
        public SettingsDal(DbContextOptions<StorageContext> options) : base(options) { }

        #endregion

        #region Methods

        /// <summary>
        /// Get a setting by it's key.
        /// </summary>
        /// <param key="key">The key of the setting.</param>
        /// <returns></returns>
        public Setting GetSetting(string key)
        {
            return Db.Settings.Find(key);
        }

        /// <summary>
        /// Get all the settings.
        /// </summary>
        /// <returns></returns>
        public ICollection<Setting> GetSettings()
        {
            return Db.Settings.ToList();
        }

        /// <summary>
        /// Ensure default settings exist in database.
        /// </summary>
        public void EnsureSettingsExist()
        {
            var settings = new List<Setting>
            {
                CreateStringSetting("firefly_url", ""),
                CreateStringSetting("firefly_access_token", ""),
                CreateStringSetting("nordigen_base_url", "https://ob.nordigen.com"),
                CreateStringSetting("nordigen_secret_id", ""),
                CreateStringSetting("nordigen_secret_key", ""),
                CreateIntegerSetting("import_days", 10),
                CreateBooleanSetting("extended_notes", false),
                CreateBooleanSetting("automatic_import", false),
                CreateBooleanSetting("filter_authorisations", true)
            };

            var existingSettingsKeys = Db.Settings.Select(s => s.Key).ToList();
            var newSettings = settings.Where(s => !existingSettingsKeys.Contains(s.Key)).ToList();

            Db.Settings.AddRange(newSettings);
            Db.SaveChanges();
        }

        #region Static Methods

        private static Setting CreateBooleanSetting(string key, bool defaultValue)
        {
            return new Setting
            {
                Key = key,
                ValueType = "bool",
                BooleanValue = defaultValue
            };
        }

        private static Setting CreateIntegerSetting(string key, int defaultValue)
        {
            return new Setting
            {
                Key = key,
                ValueType = "int",
                IntValue = defaultValue
            };
        }

        private static Setting CreateStringSetting(string key, string defaultValue)
        {
            return new Setting
            {
                Key = key,
                ValueType = "string",
                StringValue = defaultValue
            };
        }

        private static Setting CreateTimeSpanSetting(string key, TimeSpan defaultValue)
        {
            return new Setting
            {
                Key = key,
                ValueType = "timespan",
                TimeSpanValue = defaultValue
            };
        }

        #endregion

        #endregion
    }
}