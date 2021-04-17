using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace TestOkur.HealthCheck
{
    public static class HealthCheckEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapReadiness(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks("/api/ready", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }


        public static IEndpointConventionBuilder MapLiveness(this IEndpointRouteBuilder endpoints)
        {
            var healthChecks = new HashSet<string> { "AlwaysHealthy" };

            return endpoints.MapHealthChecks("/api/live", new HealthCheckOptions
            {
                Predicate = r => healthChecks.Contains(r.Name)
            });
        }
    }
}