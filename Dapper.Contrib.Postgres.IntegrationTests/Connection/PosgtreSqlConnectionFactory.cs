using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dapper.Contrib.Postgres.IntegrationTests.Connection
{
    public class PosgtreSqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public PosgtreSqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connString = _configuration.GetValue<string>("PosgtreSqlConnectionString");
            
            return new NpgsqlConnection(connString);
        }
    }
}