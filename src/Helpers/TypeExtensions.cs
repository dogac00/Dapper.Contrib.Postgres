using System;
using System.Reflection;

namespace Dapper.Contrib.Postgres.Helpers
{
    public static class TypeExtensions
    {
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) != null;
        }
        
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute(typeof(T)) as T;
        }
    }
}