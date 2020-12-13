using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [Table("MyTestTable")]
    public class TestType10
    {
        [Column("\"MyIntField\"")]
        public int IntField { get; set; }
        [Column("\"MyDateTimeField\"")]
        public DateTime DateTimeField { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists MyTestTable
                    (
                        ""MyIntField"" integer,
                        ""MyDateTimeField"" timestamp
                    );";
        }

        public static string DropTableScript()
        {
            return @"drop table if exists MyTestTable;";
        }
    }
}