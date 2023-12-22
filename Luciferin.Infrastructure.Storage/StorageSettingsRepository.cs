using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luciferin.Application.Abstractions.Repositories;
using Luciferin.Core.Entities;
using Luciferin.Infrastructure.Storage.Context;

namespace Luciferin.Infrastructure.Storage;

public class StorageSettingsRepository : ISettingRepository
{
    protected readonly StorageContext Db;

    protected StorageSettingsRepository(StorageContext context)
    {
        Db = context;
    }

    public IQueryable<Setting> Entities { get; }

    public Task<Setting> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Setting>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Setting> AddAsync(Setting entity)
    {
        throw new NotImplementedException();
    }

    public Task<Setting> AddListAsync(IEnumerable<Setting> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Setting entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Setting entity)
    {
        throw new NotImplementedException();
    }
}