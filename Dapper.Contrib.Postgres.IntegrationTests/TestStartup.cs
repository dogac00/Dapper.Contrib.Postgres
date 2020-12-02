using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dapper.Contrib.Postgres.IntegrationTests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }
        
        public void Configure(IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/healthcheck");
        }
    }
}