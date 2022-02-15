using Luciferin.BusinessLayer.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Luciferin.BusinessLayer.Helpers
{
    public static class ServiceCollectionExtensions
    {
        #region Methods

        #region Static Methods

        public static void AddMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(StatisticsProfile));
        }

        #endregion

        #endregion
    }
}