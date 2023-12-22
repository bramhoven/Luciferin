using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.Infrastructure.Storage.Context;
using Luciferin.Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.Infrastructure.Storage
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
                CreateStringSetting("Firefly", "firefly_url", ""),
                CreateStringSetting("Firefly", "firefly_access_token", ""),
                
                CreateStringSetting("GoCardless", "BaseUrl", "https://bankaccountdata.gocardless.com/"),
                CreateStringSetting("GoCardless", "SecretId", ""),
                CreateStringSetting("GoCardless", "SecretKey", ""),
                
                CreateIntegerSetting("Luciferin", "ImportDays", 10),
                CreateBooleanSetting("Luciferin", "ExtendedNotes", false),
                CreateBooleanSetting("Luciferin", "AutomaticImport", false),
                CreateBooleanSetting("Luciferin", "FilterAuthorisations", true),
                CreateIntegerSetting("Luciferin", "NotificationMethod", 0),
                
                CreateStringSetting("Mail", "NotificationEmail", ""),
                CreateStringSetting("Mail", "FromEmail", "no-reply@luciferin.local"),
                CreateStringSetting("Mail", "Host", ""),
                CreateIntegerSetting("Mail", "Port", 25),
                CreateStringSetting("Mail", "Username", ""),
                CreateStringSetting("Mail", "Password", ""),
                CreateBooleanSetting("Mail", "EnableSsl", false)
            };

            var existingSettingsKeys = Db.Settings.Select(s => s.Key).ToList();
            var newSettings = settings.Where(s => !existingSettingsKeys.Contains(s.Key)).ToList();

            Db.Settings.AddRange(newSettings);
            Db.SaveChanges();
        }

        #region Static Methods

        private static Setting CreateBooleanSetting(string category, string key, bool defaultValue)
        {
            return new Setting
            {
                Key = key,
                Category = category,
                ValueType = "bool",
                BooleanValue = defaultValue
            };
        }

        private static Setting CreateIntegerSetting(string category, string key, int defaultValue)
        {
            return new Setting
            {
                Key = key,
                Category = category,
                ValueType = "int",
                IntValue = defaultValue
            };
        }

        private static Setting CreateStringSetting(string category, string key, string defaultValue)
        {
            return new Setting
            {
                Key = key,
                Category = category,
                ValueType = "string",
                StringValue = defaultValue
            };
        }

        private static Setting CreateTimeSpanSetting(string category, string key, TimeSpan defaultValue)
        {
            return new Setting
            {
                Key = key,
                Category = category,
                ValueType = "timespan",
                TimeSpanValue = defaultValue
            };
        }

        #endregion

        #endregion
    }
}