using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [Table("TestType5")]
    public class TestType5
    {
        [Key]
        [Column("MyData")]
        public string Data { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists TestType5 
                     (
                        MyData text
                     );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists TestType5";
        }
    }
}