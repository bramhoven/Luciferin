using Luciferin.Application.Abstractions.Providers;
using Luciferin.Infrastructure.GoCardless.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Luciferin.Infrastructure.GoCardless.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGoCardless(this IServiceCollection services)
    {
        services.AddTransient<IAccountProvider, GoCardlessAccountProvider>();
        services.AddTransient<IGoCardlessService, GoCardlessService>();
    }
}