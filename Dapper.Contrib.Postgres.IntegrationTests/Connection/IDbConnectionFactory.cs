using System.Data;

namespace Dapper.Contrib.Postgres.IntegrationTests.Connection
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}