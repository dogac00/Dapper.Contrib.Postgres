using System.Reflection;

namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class AttributeExtensions
    {
        public static bool HasAttribute<T>(this PropertyInfo property)
        {
            return property.GetCustomAttribute(typeof(T)) != null;
        }
    }
}