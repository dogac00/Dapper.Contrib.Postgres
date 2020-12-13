using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("TestType9")]
    public class TestType9
    {
        [Key]
        [Column("Name")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists TestType9
                    (
                        Name text constraint test_type9pk primary key,
                        LastName text
                    );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists TestType9;";
        }
    }
}