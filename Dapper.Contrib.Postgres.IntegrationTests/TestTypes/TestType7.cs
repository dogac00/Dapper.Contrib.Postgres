using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [Table("MyTestType7")]
    public class TestType7
    {
        [Key]
        [AutoIncrement]
        public long LongField { get; set; }
        [Column("MyStringField")]
        public string StringField { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists MyTestType7 
                    (
                        LongField bigserial constraint test_type7_pk primary key,
                        MyStringField text
                    );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists MyTestType7;";
        }
    }
}