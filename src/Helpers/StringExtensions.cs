using System.Linq;

namespace Dapper.Contrib.Postgres.Helpers
{
    public static class StringExtensions
    {
        public static string AddQuotes(this string value)
        {
            return $"\"{value}\"";
        }
        
        public static string RemoveWhitespace(this string value)
        {
            return new string(value
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}