using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestClassWithQuotedIdentifiersAttribute
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}