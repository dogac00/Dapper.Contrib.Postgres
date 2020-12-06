using System.Data;
using System.Threading.Tasks;

namespace Dapper.Contrib.Postgres.IntegrationTests.Helpers
{
    public static class DbConnectionExtensions
    {
        public static async Task DropTable(this IDbConnection connection, string tableName)
        {
            await connection.ExecuteAsync("DROP TABLE IF EXISTS " + tableName + ";");
        }
    }
}