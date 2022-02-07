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
            var entity = _settingsDal.GetSetting(setting.Key);

            switch (setting.ValueType)
            {
                case ValueType.Boolean when setting is BooleanSetting settingBoolean:
                    entity.BooleanValue = settingBoolean.Value;
                    break;
                case ValueType.Integer when setting is IntegerSetting settingInteger:
                    entity.IntValue = settingInteger.Value;
                    break;
                case ValueType.String when setting is StringSetting settingString:
                    entity.StringValue = settingString.Value ?? string.Empty;
                    break;
                case ValueType.TimeSpan when setting is TimeSpanSetting settingTimeSpan:
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
            switch (entity.ValueType)
            {
                case "bool":
                    return new BooleanSetting(entity.Key, entity.BooleanValue.Value);
                case "int":
                    return new IntegerSetting(entity.Key, entity.IntValue.Value);
                case "timespan":
                    return new TimeSpanSetting(entity.Key, entity.TimeSpanValue.Value);
                case "string":
                    return new StringSetting(entity.Key, entity.StringValue);
                default:
                    throw new InvalidOperationException();
            }
        }

        #endregion

        #endregion
    }
}