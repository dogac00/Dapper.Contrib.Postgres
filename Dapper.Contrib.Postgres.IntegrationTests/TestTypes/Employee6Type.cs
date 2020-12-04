using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class Employee6Type
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}