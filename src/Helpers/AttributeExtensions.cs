using System;
using System.Reflection;

namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class AttributeExtensions
    {
        public static bool HasAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return GetAttribute<T>(property) != null;
        }
        
        public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttribute(typeof(T)) as T;
        }
    }
}