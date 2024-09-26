using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DotNet8WebApi.HealthCheckExample.Dependencies
{
    public static class DbHealthCheckDependency
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString("DbConnection")!, healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" });

            //services.AddHealthChecksUI();
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("Sample API", "/api/health"); //map health check api    

            })
                .AddInMemoryStorage();

            return services;
        }
    }
}
