using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    [UseQuotedIdentifiers]
    public class TestClassWithoutKeyAttributeWithColumnAttribute
    {
        [Column("MyId")]
        public string Id { get; set; }
    }
}