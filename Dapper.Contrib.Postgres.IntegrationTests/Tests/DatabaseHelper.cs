using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.IntegrationTests.Helpers;
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
        
        public static async Task DropTables(IDbConnection connection)
        {
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employees""");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS MyEmployees");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS EmployeeTable");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS Employee4s");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employee5""");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employee6Types""");
        }
        
        public static async Task CreateTables(IDbConnection connection)
        {
            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""Employees""
                                              (
                                                ""Id"" bigint primary key not null,
                                                ""Name"" text
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS MyEmployees
                                              (
                                                MyFirstName text,
                                                MyLastName text
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS EmployeeTable
                                              (
                                                ""EmployeeId"" bigint,
                                                EmployeeName text
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Employee4s
                                              (
                                                Id bigint,
                                                Name text,
                                                DateUnixTimestamp bigint
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""Employee5""
                                              (
                                                ""Id"" bigint,
                                                ""Name"" text,
                                                ""Money"" numeric
                                              )");
            
            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""Employee6Types""
                                              (
                                                ""Id"" bigint,
                                                ""Name"" text,
                                                ""Balance"" numeric
                                              )");
        }
    }
}