using Luciferin.DataLayer.Storage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Luciferin.BusinessLayer.Settings;

public class LuciferinConfigurationSource : IConfigurationSource
{
    private readonly DbContextOptions<StorageContext> _options;

    public LuciferinConfigurationSource(DbContextOptions<StorageContext> options)
    {
        _options = options;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder) => new LuciferinConfigurationProvider(_options);
}