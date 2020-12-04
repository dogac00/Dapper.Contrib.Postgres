using System.Linq;

namespace Dapper.Contrib.Postgres.Tests.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string value)
        {
            return new string(value
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}