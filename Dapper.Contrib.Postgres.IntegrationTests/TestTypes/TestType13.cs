using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType13
    {
        [Column("MyId")]
        public string Id { get; set; }
    }
}