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

        public static string CreateTableScript()
        {
            return @"create table if not exists ""TestType8s""
                    (
                        MyFirstName text,
                        ""LastName"" text constraint test_type8_pk primary key
                    );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists ""TestType8s"";";
        }
    }
}