using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType12
    {
        [AutoIncrement]
        public string Id { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists ""TestType12s""
                    (
                        ""Id"" text constraint test_type12_pk primary key
                    );";
        }

        public static string DropTableScript()
        {
            return @"drop table if exists ""TestType12s"";";
        }
    }
}