using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.Attributes;
using Dapper.Contrib.Postgres.Helpers;
using Pluralize.NET.Core;

namespace Dapper.Contrib.Postgres
{
    public static class Extensions
    {
        private static readonly Pluralizer _pluralizer = new Pluralizer();
        
        public static async Task<long> InsertAsync<T>(this IDbConnection connection, T entity)
        {
            var sql = GetInsertSql<T>();

            return await connection.QueryFirstOrDefaultAsync<long>(sql, entity);
        }
        
        public static long Insert<T>(this IDbConnection connection, T entity)
        {
            var sql = GetInsertSql<T>();

            return connection.QueryFirstOrDefault<long>(sql, entity);
        }
        
        public static string GetInsertSql<T>()
        {
            var insertInto = @"INSERT INTO";
            var tableName = GetTableName<T>();
            var columnNames = GetColumnNames<T>();
            var columns = "(" + string.Join(',', columnNames) + ")";
            var values = "VALUES";
            var parameters = "(" + string.Join(',', GetParameters<T>()) + ")";
            var keyName = GetKeyName<T>();
            var returning = keyName != null ? $"RETURNING {keyName}" : "";

            return $"{insertInto} {tableName} {columns} {values} {parameters} {returning};";
        }

        public static List<string> GetColumnNames<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(GetColumnName<T>)
                .ToList();
        }

        public static string GetKeyName<T>()
        {
            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var idProperty = properties.FirstOrDefault(p =>
                p.Name == "Id" || p.Name == "\"Id\"");

            if (idProperty != null)
            {
                return GetColumnName<T>(idProperty);
            }

            var keyProperty = properties.FirstOrDefault(p =>
                p.GetCustomAttributes(typeof(KeyAttribute), false).Any());

            if (keyProperty != null)
            {
                return GetColumnName<T>(keyProperty);
            }

            return null;
        }

        public static List<string> GetParameters<T>()
        {
            return typeof(T)
                .GetProperties()
                .Select(p => p.Name)
                .Select(p => "@" + p)
                .ToList();
        }
        
        public static string GetColumnName<T>(PropertyInfo property)
        {
            var columnAttribute = AttributeHelper.GetColumnAttribute(property);

            if (columnAttribute != null)
            {
                return columnAttribute.Name;
            }
            
            var useQuotesAttribute = AttributeHelper.GetUseQuotedIdentifiersAttribute<T>();
            
            if (useQuotesAttribute != null)
            {
                return property.Name.AddQuotes();
            }

            return property.Name;
        }

        public static string GetTableName<T>()
        {
            var typeName = typeof(T).Name;
            var pluralTypeName = _pluralizer.Pluralize(typeName);
            var tableAttribute = AttributeHelper.GetTableAttribute<T>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }
            
            var useQuotesAttributes = AttributeHelper.GetUseQuotedIdentifiersAttribute<T>();

            if (useQuotesAttributes != null)
            {
                return pluralTypeName.AddQuotes();
            }

            return pluralTypeName;
        }
    }
}
