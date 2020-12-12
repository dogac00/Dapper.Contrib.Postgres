using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    public class TestType4
    {
        [Column("BirthDate")]
        public DateTime BirthDateColumn { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists TestType4s
                    (
                        BirthDate timestamp
                    );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists TestType4s;";
        }
    }
}