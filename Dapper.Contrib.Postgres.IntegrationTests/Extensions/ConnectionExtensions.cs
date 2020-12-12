using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.Attributes;

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
            var tableAttribute = GetAttribute<TableAttribute>(typeof(T));

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            if (HasAttribute<UseQuotedIdentifiersAttribute>(typeof(T)))
            {
                return '"' + pluralTypeName + '"';
            }

            return pluralTypeName;
        }

        private static bool HasAttribute<T>(Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) != null;
        }

        private static T GetAttribute<T>(Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) as T;
        }
    }
}