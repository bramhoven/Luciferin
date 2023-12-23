namespace Luciferin.Infrastructure.Storage.Mappers;

using System;
using AutoMapper;
using Core.Abstractions;
using Core.Entities.Settings;
using Setting = Entities.Setting;

public class SettingProfile : Profile
{
    public SettingProfile()
    {
        CreateMap<Setting, ISetting>()
            .ConstructUsing(s => CreateInstance(s))
            .BeforeMap((s, i, c) => c.Mapper.Map(s, i));

        CreateMap<Setting, BooleanSetting>();
        CreateMap<Setting, IntegerSetting>();
        CreateMap<Setting, TimeSpanSetting>();
        CreateMap<Setting, StringSetting>();
    }

    private static ISetting CreateInstance(Setting setting)
    {
        return setting.ValueType switch
        {
            "bool" => new BooleanSetting(setting.Category, setting.Key, setting.BooleanValue),
            "int" => new IntegerSetting(setting.Category, setting.Key, setting.IntValue),
            "timespan" => new TimeSpanSetting(setting.Category, setting.Key, setting.TimeSpanValue),
            "string" => new StringSetting(setting.Category, setting.Key, setting.StringValue),
            _ => throw new InvalidOperationException()
        };
    }
}