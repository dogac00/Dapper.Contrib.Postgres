using System.Data;
using System.Linq;
using Dapper.Contrib.Postgres.Attributes;
using Pluralize.NET.Core;

namespace Dapper.Contrib.Postgres
{
    public static class Extensions
    {
        private static readonly Pluralizer _pluralizer = new Pluralizer();
        
        public static long Insert<T>(this IDbConnection connection, T entity)
        {
            return 0;
        }

        public static string GetTableName<T>()
        {
            var tableAttributes = typeof(T)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .Cast<TableAttribute>()
                .ToList();

            if (!tableAttributes.Any())
            {
                var typeName = typeof(T).Name;
                return _pluralizer.Pluralize(typeName);
            }

            return tableAttributes
                .First()
                .Name;
        }
    }
}