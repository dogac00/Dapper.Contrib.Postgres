using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType12
    {
        public string Id { get; set; }
    }
}