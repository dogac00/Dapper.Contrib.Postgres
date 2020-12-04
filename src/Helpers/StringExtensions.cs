namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class StringExtensions
    {
        public static string AddQuotes(this string value)
        {
            return $"\"{value}\"";
        }
    }
}