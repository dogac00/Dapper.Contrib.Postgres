using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dapper.Contrib.Postgres.IntegrationTests.Connection
{
    public class PostgreSqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public PostgreSqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connString = _configuration.GetValue<string>("PostgreSqlConnectionString");
            
            return new NpgsqlConnection(connString);
        }
    }
}