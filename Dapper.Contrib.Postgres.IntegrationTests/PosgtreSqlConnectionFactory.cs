using System.Data.Common;
using System.Data.Entity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dapper.Contrib.Postgres.IntegrationTests
{
    public class PosgtreSqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public PosgtreSqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            var connString = _configuration.GetValue<string>("PosgtreSqlConnectionString");
            
            return new NpgsqlConnection(connString);
        }
    }
}