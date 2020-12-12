using System;
using System.Reflection;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class PropertyExtensions
    {
        public static bool IsIdProperty(this PropertyInfo propertyInfo)
        {
            return propertyInfo.Name == "Id" ||
                   propertyInfo.HasAttribute<KeyAttribute>();
        }

        public static string GetColumnName(this PropertyInfo property, Type type)
        {
            var columnAttribute = property.GetAttribute<ColumnAttribute>();

            if (columnAttribute != null)
            {
                return columnAttribute.Name;
            }

            if (type.HasAttribute<UseQuotedIdentifiersAttribute>())
            {
                return property.Name.AddQuotes();
            }

            return property.Name;
        }
    }
}