using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType8
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}