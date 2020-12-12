using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.Attributes;
using Dapper.Contrib.Postgres.Helpers;

namespace Dapper.Contrib.Postgres.IntegrationTests.Extensions
{
    public static class ConnectionExtensions
    {
        public static async Task<List<T>> GetAll<T>(this IDbConnection connection)
        {
            var tableName = GetTableName<T>();

            return (await connection.QueryAsync<T>($@"select * from {tableName}"))
                .ToList();
        }
        
        private static string GetTableName<T>()
        {
            var typeName = typeof(T).Name;
            var pluralTypeName = typeName + "s";
            var tableAttribute = typeof(T).GetAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            if (typeof(T).HasAttribute<UseQuotedIdentifiersAttribute>())
            {
                return '"' + pluralTypeName + '"';
            }

            return pluralTypeName;
        }
    }
}