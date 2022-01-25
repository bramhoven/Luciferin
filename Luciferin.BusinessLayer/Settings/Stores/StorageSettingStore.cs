using System;
using System.Collections.Generic;
using System.Linq;
using Luciferin.BusinessLayer.Settings.Models;
using Luciferin.DataLayer.Storage;
using Luciferin.DataLayer.Storage.Entities;
using ValueType = Luciferin.BusinessLayer.Settings.Enums.ValueType;

namespace Luciferin.BusinessLayer.Settings.Stores
{
    public class StorageSettingStore : ISettingsStore
    {
        #region Fields

        private readonly SettingsDal _settingsDal;

        #endregion

        #region Constructors

        public StorageSettingStore(SettingsDal settingsDal)
        {
            _settingsDal = settingsDal;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public ISetting GetSetting(string name)
        {
            var entity = _settingsDal.GetSetting(name);
            return MapSetting(entity);
        }

        /// <inheritdoc />
        public ICollection<ISetting> GetSettings()
        {
            var entities = _settingsDal.GetSettings();
            return entities.Select(MapSetting).ToList();
        }

        /// <inheritdoc />
        public void UpdateSetting(ISetting setting)
        {
            if (setting.HasValue)
                throw new InvalidOperationException(nameof(setting));
            
            var entity = _settingsDal.GetSetting(setting.Name);

            switch (setting.ValueType)
            {
                case ValueType.Boolean when setting is SettingBoolean settingBoolean:
                    entity.BooleanValue = settingBoolean.Value;
                    break;
                case ValueType.Integer when setting is SettingInteger settingInteger:
                    entity.IntValue = settingInteger.Value;
                    break;
                case ValueType.String when setting is SettingString settingString:
                    entity.StringValue = settingString.Value;
                    break;
                case ValueType.TimeSpan when setting is SettingTimeSpan settingTimeSpan:
                    entity.TimeSpanValue = settingTimeSpan.Value;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            _settingsDal.Save();
        }

        #region Static Methods

        private static ISetting MapSetting(Setting entity)
        {
            if (entity.BooleanValue.HasValue)
                return new SettingBoolean(entity.Id, entity.Name, entity.BooleanValue.Value);

            if (entity.IntValue.HasValue)
                return new SettingInteger(entity.Id, entity.Name, entity.IntValue.Value);

            if (entity.TimeSpanValue.HasValue)
                return new SettingTimeSpan(entity.Id, entity.Name, entity.TimeSpanValue.Value);

            return new SettingString(entity.Id, entity.Name, entity.StringValue);
        }

        #endregion

        #endregion
    }
}