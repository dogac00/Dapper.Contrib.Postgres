using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    public class TestType1
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists TestType1s 
                        (
                            Id bigserial constraint test_type1_pk primary key,
                            Name text
                        );";
        }

        public static string DropTableScript()
        {
            return @"drop table if exists TestType1s;";
        }
    }
}