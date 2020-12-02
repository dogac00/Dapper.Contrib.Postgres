using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("MyTable")]
    public class TestClassWithQuotedIdentifiersAndTableAttribute
    {
        [Column("FirstNameColumn")]
        public string FirstName { get; set; }
        [Column("SecondNameColumn")]
        public string LastName { get; set; }
    }
}