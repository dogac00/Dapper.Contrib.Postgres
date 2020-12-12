using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("TestType6")]
    public class TestType6
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists TestType6 
                    (
                        Id integer,
                        Name text
                    );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists TestType6;";
        }
    }
}