namespace Luciferin.Infrastructure.GoCardless.Extensions;

using Abstractions;
using Application.Abstractions.Providers;
using Microsoft.Extensions.DependencyInjection;
using Providers;

public static class ServiceCollectionExtensions
{
    public static void AddGoCardless(this IServiceCollection services)
    {
        services.AddTransient<IRequisitionProvider, GoCardlessRequisitionProvider>();
        services.AddTransient<IGoCardlessService, GoCardlessService>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}