using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    public class TestType2
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        
        public static string CreateTableScript()
        {
            return @"create table if not exists TestType2s 
                        (
                            Id text constraint test_type2_pk primary key,
                            Name text
                        );";
        }

        public static string DropTableScript()
        {
            return @"drop table if exists TestType2s;";
        }
    }
}