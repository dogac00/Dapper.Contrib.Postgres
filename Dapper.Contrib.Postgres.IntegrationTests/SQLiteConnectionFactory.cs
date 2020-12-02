using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;

namespace Dapper.Contrib.Postgres.IntegrationTests
{
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new SQLiteConnection("Data Source=TestDatabase; Mode=Memory; Cache=Shared");
        }
    }
}