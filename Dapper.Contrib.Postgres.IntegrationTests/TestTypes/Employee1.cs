using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class Employee1
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}