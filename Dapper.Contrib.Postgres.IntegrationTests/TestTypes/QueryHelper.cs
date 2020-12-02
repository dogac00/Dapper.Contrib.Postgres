using System;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    public static class QueryHelper
    {
        private static string RemoveReturningClause(string sql)
        {
            var returningIndex = sql.IndexOf("RETURNING ", StringComparison.Ordinal);

            return sql.Substring(0, returningIndex);
        }

        public static string GetInsertSqlForSqLite<T>()
        {
            var postgresInsert = Extensions.GetInsertSql<T>();

            return RemoveReturningClause(postgresInsert);
        }
    }
}