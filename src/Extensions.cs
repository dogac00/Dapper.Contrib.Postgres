using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Contrib.Postgres.Attributes;
using Dapper.Contrib.Postgres.Helpers;

namespace Dapper.Contrib.Postgres
{
    public static class Extensions
    {
        /// <summary>
        /// Inserts the given entity.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The Id of the inserted entity.</returns>
        public static async Task InsertAsync<T>(this IDbConnection connection, T entity)
        {
            var sql = GetInsertSql<T>();

            var id = await connection.QueryFirstOrDefaultAsync<string>(sql, entity);

            SetId(entity, id);
        }
        
        /// <summary>
        /// Inserts the given entity.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The Id of the inserted entity.</returns>
        public static void Insert<T>(this IDbConnection connection, T entity)
        {
            InsertAsync(connection, entity)
                .GetAwaiter()
                .GetResult();
        }

        private static void SetId<T>(T entity, string id)
        {
            var idProperty = GetKeyProperty<T>();
            
            if (idProperty != null)
            {
                var idType = idProperty.PropertyType;
                var result = Convert.ChangeType(id, idType);
                idProperty.SetValue(entity, result);
            }
        }
        
        private static string GetInsertSql<T>()
        {
            var insertInto = @"INSERT INTO";
            var tableName = GetTableName<T>();
            var columns = "(" + string.Join(',', GetColumnNames<T>()) + ")";
            var values = "VALUES";
            var parameters = "(" + string.Join(',', GetParameters<T>()) + ")";
            var keyName = GetKeyName<T>();
            var returning = keyName != null ? $"RETURNING {keyName}" : "";

            return $"{insertInto} {tableName} {columns} {values} {parameters} {returning};";
        }

        private static List<string> GetColumnNames<T>()
        {
            return typeof(T)
                .GetPublicProperties()
                .Where(p => !p.HasAttribute<AutoIncrementAttribute>())
                .Select(GetColumnName<T>)
                .ToList();
        }

        private static string GetKeyName<T>()
        {
            var properties = typeof(T)
                .GetPublicProperties();

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

        private static PropertyInfo GetKeyProperty<T>()
        {
            return typeof(T)
                .GetPublicProperties()
                .FirstOrDefault(p => p.IsIdProperty());
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
        
        private static string GetColumnName<T>(PropertyInfo property)
        {
            var columnAttribute = property.GetAttribute<ColumnAttribute>();

            if (columnAttribute != null)
            {
                return columnAttribute.Name;
            }

            if (typeof(T).HasAttribute<UseQuotedIdentifiersAttribute>())
            {
                return property.Name.AddQuotes();
            }

            return property.Name;
        }

        private static string GetTableName<T>()
        {
            var typeName = typeof(T).Name;
            var pluralTypeName = Pluralizer.Pluralize(typeName);
            var tableAttribute = typeof(T).GetAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            if (typeof(T).HasAttribute<UseQuotedIdentifiersAttribute>())
            {
                return pluralTypeName.AddQuotes();
            }

            return pluralTypeName;
        }
    }
}
