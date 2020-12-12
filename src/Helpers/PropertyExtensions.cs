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
    }
}