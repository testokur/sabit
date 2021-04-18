using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestOkur.Configuration.Extensions;
using TestOkur.HealthCheck;
using TestOkur.Sabit.Configuration;
using TestOkur.Sabit.Extensions;
using TestOkur.Sabit.Infrastructure;

namespace TestOkur.Sabit
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private AppConfiguration _appConfiguration = new AppConfiguration();
        private readonly IHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.BindConfig(_configuration, out _appConfiguration)
                .AddAuthorization()
                .AddHealthChecks(_appConfiguration)
                .AddMemoryCache()
                .AddResponseCompression()
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                        });
                })
                .AddControllers();
            services.AddAuthentication();
            services.AddSingleton<IJsonDataSource, JsonDataSource>();
            services.Decorate<IJsonDataSource, CachedJsonDataSource>();

            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(_appConfiguration.RabbitMq.ToUri());
                });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseResponseCompression();
            app.UseCors();
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
