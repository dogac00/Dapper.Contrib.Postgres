using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType3
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }

        public static string CreateTableScript()
        {
            return @"create table if not exists ""TestType3s"" 
                    (
                        ""Name"" text,
                        ""Date"" timestamp,
                        ""Balance"" numeric
                    );";
        }
        
        public static string DropTableScript()
        {
            return @"drop table if exists ""TestType3s"";";
        }
    }
}