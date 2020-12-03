using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    [Table("MyEmployees")]
    public class Employee2
    {
        [Column("MyFirstName")]
        public string FirstName { get; set; }
        [Column("MyLastName")]
        public string LastName { get; set; }
    }
}