using Microsoft.Extensions.DependencyInjection;
using TestOkur.Sabit.Configuration;

namespace TestOkur.Sabit.Extensions
{
    public static class HealthCheckServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecks(
            this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services.AddHealthChecks();

            return services;
        }
    }
}
