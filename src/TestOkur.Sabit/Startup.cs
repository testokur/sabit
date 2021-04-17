using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestOkur.Configuration.Extensions;
using TestOkur.HealthCheck;
using TestOkur.Sabit.Configuration;
using TestOkur.Sabit.Extensions;

namespace TestOkur.Sabit
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private AppConfiguration _appConfiguration = new AppConfiguration();

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.BindConfig(_configuration, out _appConfiguration)
                .AddAuthorization()
                .AddHealthChecks(_appConfiguration)
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapLiveness();
                endpoints.MapReadiness();
            });
        }
    }
}
