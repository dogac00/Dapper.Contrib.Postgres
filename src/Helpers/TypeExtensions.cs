using System;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class TypeExtensions
    {
        public static string GetTableName(this Type type)
        {
            var pluralTypeName = Pluralizer.Pluralize(type.Name);
            var tableAttribute = type.GetAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            if (type.HasAttribute<UseQuotedIdentifiersAttribute>())
            {
                return pluralTypeName.AddQuotes();
            }

            return pluralTypeName;
        }
        
        public static string GetKeyName(this Type type)
        {
            var keyProperty = type.GetKeyProperty();

            return keyProperty == null 
                ? null 
                : keyProperty.GetColumnName(type);
        }

        public static PropertyInfo GetKeyProperty(this Type type)
        {
            return type
                .GetPublicProperties()
                .FirstOrDefault(p => p.IsIdProperty());
        }
        
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

        public static bool IsIntegralType(this Type type)
        {
            return type == typeof(sbyte) ||
                   type == typeof(byte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong);

        }
    }
}