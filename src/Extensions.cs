using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.Attributes;
using Dapper.Contrib.Postgres.Helpers;

namespace Dapper.Contrib.Postgres
{
    public static class Extensions
    {
        public static async Task InsertAsync<T>(this IDbConnection connection, T entity)
        {
            ValidateAutoIncrementAttributes<T>();
            
            var sql = GetInsertSql<T>();

            var id = await connection.QueryFirstOrDefaultAsync<string>(sql, entity);

            TrySetId(entity, id);
        }
        
        public static void Insert<T>(this IDbConnection connection, T entity)
        {
            InsertAsync(connection, entity)
                .GetAwaiter()
                .GetResult();
        }

        private static void ValidateAutoIncrementAttributes<T>()
        {
            var autoIncrementProperties = typeof(T)
                .GetPublicProperties()
                .Where(p => p.HasAttribute<AutoIncrementAttribute>())
                .ToList();

            if (autoIncrementProperties.Any(p => !p.PropertyType.IsIntegralType()))
            {
                throw new InvalidOperationException("Only integral types can be auto increment.");
            }
        }
        
        private static string GetInsertSql<T>()
        {
            var insertInto = @"INSERT INTO";
            var tableName = typeof(T).GetTableName();
            var columns = "(" + string.Join(',', GetColumnNames<T>()) + ")";
            var values = "VALUES";
            var parameters = "(" + string.Join(',', GetParameters<T>()) + ")";
            var keyName = typeof(T).GetKeyName();
            var returning = keyName != null ? $"RETURNING {keyName}" : "";

            return $"{insertInto} {tableName} {columns} {values} {parameters} {returning};";
        }

        private static void TrySetId<T>(T entity, string id)
        {
            var keyProperty = typeof(T).GetKeyProperty();

            if (keyProperty == null ||
                id == null)
            {
                return;
            }

            var idType = keyProperty.PropertyType;
            var result = Convert.ChangeType(id, idType);
            keyProperty.SetValue(entity, result);
        }

        private static List<string> GetColumnNames<T>()
        {
            return typeof(T)
                .GetPublicProperties()
                .Where(p => !p.HasAttribute<AutoIncrementAttribute>())
                .Select(p => p.GetColumnName(typeof(T)))
                .ToList();
        }

        private static List<string> GetParameters<T>()
        {
            return typeof(T)
                .GetProperties()
                .Where(p => !p.HasAttribute<AutoIncrementAttribute>())
                .Select(p => p.Name)
                .Select(p => "@" + p)
                .ToList();
        }
    }
}