namespace Dapper.Contrib.Postgres.Helpers
{
    internal static class Pluralizer
    {
        public static string Pluralize(string input)
        {
            return input + "s";
        }
    }
}