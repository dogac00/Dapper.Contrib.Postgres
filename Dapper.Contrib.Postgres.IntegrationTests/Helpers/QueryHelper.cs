using System;

namespace Dapper.Contrib.Postgres.IntegrationTests.Helpers
{
    public static class QueryHelper
    {
        private static string TryRemoveReturningClause(string sql)
        {
            var returningIndex = sql.IndexOf("RETURNING ", StringComparison.Ordinal);

            return returningIndex == -1 
                ? sql 
                : sql.Substring(0, returningIndex);
        }

        public static string GetInsertSqlForSqLite<T>()
        {
            var postgresInsert = Extensions.GetInsertSql<T>();

            return TryRemoveReturningClause(postgresInsert);
        }
    }
}