using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    [UseQuotedIdentifiers]
    public class TestClassWithoutKeyAttribute
    {
        public string Id { get; set; }
    }
}