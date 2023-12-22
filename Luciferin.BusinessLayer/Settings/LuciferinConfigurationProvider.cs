using System;
using System.Linq;
using Luciferin.BusinessLayer.Settings.Models;
using Luciferin.BusinessLayer.Settings.Stores;
using Luciferin.Infrastructure.Storage;
using Luciferin.Infrastructure.Storage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ValueType = Luciferin.BusinessLayer.Settings.Enums.ValueType;

namespace Luciferin.BusinessLayer.Settings;

public class LuciferinConfigurationProvider : ConfigurationProvider
{
    private readonly DbContextOptions<StorageContext> _options;

    public LuciferinConfigurationProvider(DbContextOptions<StorageContext> options)
    {
        _options = options;
    }

    public override void Load()
    {
        var dal = new SettingsDal(_options);

        try
        {
            dal.EnsureSettingsExist();
        }
        catch (Exception)
        {
            return;
        }

        var manager = new SettingsManager(new StorageSettingStore(dal));

        var settings = manager.GetSettings();

        Data = settings.ToDictionary(s => $"{s.Category}:{s.Key}", s =>
        {
            switch (s.ValueType)
            {
                case ValueType.Boolean when s is BooleanSetting settingBoolean:
                    return settingBoolean.Value.ToString().ToLower();
                case ValueType.Integer when s is IntegerSetting settingInteger:
                    return settingInteger.Value.ToString();
                case ValueType.String when s is StringSetting settingString:
                    return settingString.Value;
                case ValueType.TimeSpan when s is TimeSpanSetting settingTimeSpan:
                    return settingTimeSpan.Value.ToString();
                default:
                    return string.Empty;
            }
        });
    }
}