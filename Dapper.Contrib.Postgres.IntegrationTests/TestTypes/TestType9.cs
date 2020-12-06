using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("MyTable")]
    public class TestType9
    {
        [Column("FirstNameColumn")]
        public string FirstName { get; set; }
        [Column("SecondNameColumn")]
        public string LastName { get; set; }
    }
}