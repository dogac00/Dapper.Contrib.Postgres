using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using Npgsql;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class DatabaseHelper
    {
        public static async Task InsertGeneric<T>(IDbConnection connection, T entity)
        {
            if (connection is NpgsqlConnection)
            {
                await connection.InsertAsync(entity);
            }
            else
            {
                var sql = QueryHelper.GetInsertSqlForSqLite<T>();

                await connection.ExecuteAsync(sql, entity);
            }
        }
    }
}