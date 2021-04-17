using Microsoft.Extensions.Hosting;
using Serilog;

namespace TestOkur.Logging.Extensions
{
    public static class SeriLogHostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Filter.ByExcluding("RequestPath like '/api/live%'")
                .Enrich.FromLogContext());
        }
    }
}
