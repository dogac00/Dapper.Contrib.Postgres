using System;
using System.Reflection;

namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class TypeExtensions
    {
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) != null;
        }
        
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) as T;
        }
        
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}