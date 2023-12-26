using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Luciferin.Application.Extensions;

using Abstractions.Services;
using Services;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IImportService, ImportService>();
        services.AddMediator();
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}