using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("\"Employee5\"")]
    public class TestType18
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
    }
}