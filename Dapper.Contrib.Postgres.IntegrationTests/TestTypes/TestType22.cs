using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType22
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}