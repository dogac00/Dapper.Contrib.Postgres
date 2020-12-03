using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("EmployeeTable")]
    public class Employee3
    {
        [Key] 
        public long EmployeeId { get; set; }
        [Column("EmployeeName")] 
        public string Name { get; set; }
    }
}