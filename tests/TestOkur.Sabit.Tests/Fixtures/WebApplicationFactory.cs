using System;
using System.Security.Principal;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using TestOkur.Sabit.Tests.Extensions;

namespace TestOkur.Sabit.Tests.Fixtures
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.Remove<IBus>();

                services.AddMassTransit(mt =>
                {
                    mt.UsingInMemory((context, cfg) =>
                    {
                        cfg.TransportConcurrencyLimit = 100;

                        cfg.ConfigureEndpoints(context);
                    });
                    mt.AddConsumer<ErrorConsumer>();
                });
            });
        }
    }
}
