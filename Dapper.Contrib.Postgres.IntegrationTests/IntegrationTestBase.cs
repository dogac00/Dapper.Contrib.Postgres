using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests
{
    public class IntegrationTestBase
    {
        private IServiceProvider _serviceProvider;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.UseConfiguration(configuration);
            webHostBuilder.UseStartup<TestStartup>();
            
            var testServer = new TestServer(webHostBuilder);
            _serviceProvider = testServer.Services;
        }

        protected T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}