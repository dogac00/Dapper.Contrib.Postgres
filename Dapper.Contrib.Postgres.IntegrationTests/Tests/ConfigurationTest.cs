using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class ConfigurationTest : IntegrationTestBase
    {
        private IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _configuration = GetRequiredService<IConfiguration>();
        }

        [Test]
        public void ShouldGetEnvironmentFromAppSettings()
        {
            var env = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");

            env.Should().Be("development");
        }
    }
}