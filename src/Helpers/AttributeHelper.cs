using System.Linq;
using System.Reflection;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class AttributeHelper
    {
        public static ColumnAttribute GetColumnAttribute(PropertyInfo property)
        {
            return property
                .GetCustomAttributes(typeof(ColumnAttribute), false)
                .Cast<ColumnAttribute>()
                .FirstOrDefault();
        }
        
        public static TableAttribute GetTableAttribute<T>()
        {
            return typeof(T)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .Cast<TableAttribute>()
                .FirstOrDefault();
        }
        
        public static UseQuotedIdentifiersAttribute GetUseQuotedIdentifiersAttribute<T>()
        {
            return typeof(T)
                .GetCustomAttributes(typeof(UseQuotedIdentifiersAttribute), false)
                .Cast<UseQuotedIdentifiersAttribute>()
                .FirstOrDefault();
        }
    }
}