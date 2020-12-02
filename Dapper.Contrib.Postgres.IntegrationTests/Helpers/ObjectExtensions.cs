using System.Text.Json;

namespace Dapper.Contrib.Postgres.IntegrationTests.Helpers
{
    public static class ObjectExtensions
    {
        public static bool JsonEquals<TFirst, TSecond>(this TFirst first, TSecond second)
        {
            return first.ToJson() == second.ToJson();
        }

        public static string ToJson<T>(this T value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}