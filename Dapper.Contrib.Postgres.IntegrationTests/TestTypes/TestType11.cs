using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType11
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists ""TestType11s""
                    (
                        ""Id"" integer constraint test_type11_pk primary key,
                        ""Data"" text
                    );";
        }

        public static string DropTableScript()
        {
            return @"drop table if exists ""TestType11s""";
        }
    }
}