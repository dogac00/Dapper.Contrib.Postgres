using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class QuotedEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}