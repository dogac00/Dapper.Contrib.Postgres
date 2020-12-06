using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType14
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}