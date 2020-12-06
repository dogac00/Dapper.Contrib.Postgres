namespace Dapper.Contrib.Postgres
{
    internal static class Pluralizer
    {
        public static string Pluralize(string input)
        {
            return input + "s";
        }
    }
}