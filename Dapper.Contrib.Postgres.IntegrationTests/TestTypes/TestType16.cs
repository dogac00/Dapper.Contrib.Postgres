using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [Table("MyEmployees")]
    public class TestType16
    {
        [Column("MyFirstName")]
        public string FirstName { get; set; }
        [Column("MyLastName")]
        public string LastName { get; set; }
    }
}