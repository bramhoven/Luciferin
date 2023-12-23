namespace Luciferin.Infrastructure.Storage.Extensions;

using Application.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddStorage(this IServiceCollection services)
    {
        services.AddTransient<ISettingRepository, StorageSettingsRepository>();
        services.AddTransient<ISettingRepository, StorageSettingsRepository>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}