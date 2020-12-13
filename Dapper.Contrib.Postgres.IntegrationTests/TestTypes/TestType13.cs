using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
	[UseQuotedIdentifiers]
	public class TestType13
    {
	    [AutoIncrement]
	    public long Id { get; set; }
	    public long RelatedId { get; set; }
	    public long UserId { get; set; }
	    public string UserEmail { get; set; }
	    public DateTime Date { get; set; }
	    public string Type { get; set; }
        
        public static string CreateTableScript()
        {
	        return @"create table if not exists ""TestType13s""
					(
						""Id"" bigserial not null
							constraint test_type13_pk
								primary key,
						""RelatedId"" bigint not null,
						""UserId"" bigint not null,
						""UserEmail"" varchar not null,
						""Date"" timestamp not null,
						""Type"" varchar not null
					);";
        }

        public static string DropTableScript()
        {
	        return @"drop table if exists ""TestType13s"";";
        }
    }
}