namespace Luciferin.Infrastructure.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using AutoMapper;
using Context;
using Core.Abstractions;
using Core.Entities.Settings;
using Core.Enums;
using Setting = Entities.Setting;

public class StorageSettingsRepository : ISettingRepository
{
    private readonly IMapper _mapper;
    protected readonly StorageContext Db;

    public StorageSettingsRepository(StorageContext context, IMapper mapper)
    {
        Db = context;
        _mapper = mapper;
    }

    public IQueryable<ISetting> Entities { get; }

    public async Task<ISetting> GetByKeyAsync(string key)
    {
        var setting = await Db.Settings.FindAsync(key);
        return _mapper.Map<ISetting>(setting);
    }

    public Task<List<ISetting>> GetAllAsync()
    {
        return Task.FromResult(Db.Settings.Select(_mapper.Map<ISetting>).ToList());
    }

    public async Task<ISetting> AddAsync(ISetting entity)
    {
        var dbEntity = _mapper.Map<Setting>(entity);
        await Db.Settings.AddAsync(dbEntity);
        return _mapper.Map<ISetting>(dbEntity);
    }

    public async Task<ICollection<ISetting>> AddListAsync(IEnumerable<ISetting> entities)
    {
        var dbEntities = entities.Select(_mapper.Map<Setting>).ToList();
        await Db.Settings.AddRangeAsync(dbEntities);
        return dbEntities.Select(_mapper.Map<ISetting>).ToList();
    }

    public async Task UpdateAsync(ISetting entity)
    {
        var dbEntity = await Db.Settings.FindAsync(entity.Key);
        if (dbEntity == null)
        {
            return;
        }

        switch (entity.ValueType)
        {
            case ValueTypeEnum.Boolean:
                dbEntity.BooleanValue = (entity as BooleanSetting)?.Value;
                break;
            case ValueTypeEnum.Integer:
                dbEntity.IntValue = (entity as IntegerSetting)?.Value;
                break;
            case ValueTypeEnum.String:
                dbEntity.StringValue = (entity as StringSetting)?.Value ?? string.Empty;
                break;
            case ValueTypeEnum.TimeSpan:
                dbEntity.TimeSpanValue = (entity as TimeSpanSetting)?.Value;
                break;
            default:
                throw new ArgumentOutOfRangeException($"No updating handling for ValueType {entity.ValueType}");
        }

        await Db.SaveChangesAsync();
    }

    public async Task DeleteAsync(ISetting entity)
    {
        var dbEntity = await Db.Settings.FindAsync(entity.Key);
        Db.Settings.Remove(dbEntity);
    }
}