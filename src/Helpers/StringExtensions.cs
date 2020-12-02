namespace Dapper.Contrib.Postgres.Helpers
{
    public static class StringExtensions
    {
        public static string AddQuotes(this string value)
        {
            return $"\"{value}\"";
        }
    }
}