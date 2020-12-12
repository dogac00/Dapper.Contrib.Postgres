using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType8
    {
        [Column("MyFirstName")]
        public string FirstName { get; set; }
        [Key]
        [AutoIncrement]
        public string LastName { get; set; }
    }
}