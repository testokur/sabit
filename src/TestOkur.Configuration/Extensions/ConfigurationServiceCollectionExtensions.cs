using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestOkur.Configuration.Extensions
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection BindConfig<TAppConfiguration>(
            this IServiceCollection services,
            IConfiguration configuration,
            out TAppConfiguration appConfiguration)
            where TAppConfiguration : class, new()
        {
            appConfiguration = new TAppConfiguration();
            configuration.Bind(appConfiguration);
            Validate(appConfiguration);
            services.AddSingleton(appConfiguration);

            return services;
        }

        private static void Validate<TAppConfiguration>(TAppConfiguration configuration)
        {
            var context = new ValidationContext(configuration);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(configuration, context, results, true))
            {
                return;
            }

            var errors = results.Select(r => r.ErrorMessage);
            throw new ConfigurationValidationException(
                $"Found {errors.Count()} configuration error(s) in {typeof(TAppConfiguration)}: {string.Join(",", errors)}");
        }
    }
}
