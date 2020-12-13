using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
	[UseQuotedIdentifiers]
	public class TestType14
    {
	    [AutoIncrement]
	    public long Id { get; set; }
	    public string Name { get; set; }
	    public string Expression { get; set; }
	    public int Count { get; set; }
	    public string Type { get; set; }
	    public bool IsActive { get; set; }
	    public string Deployment { get; set; }
	    public string Message { get; set; }
	    public string Channel { get; set; }
	    public string Sender { get; set; }
	    public string Host { get; set; }
	    public string UserName { get; set; }
	    public string Password { get; set; }
        
        public static string CreateTableScript()
        {
	        return @"create table if not exists ""TestType14s""
					(
						""Id"" serial not null
							constraint test_type14_pk
								primary key,
						""Name"" text,
						""Expression"" text,
						""Count"" integer,
						""Type"" text,
						""IsActive"" boolean,
						""Deployment"" text,
						""Message"" text,
						""Channel"" text,
						""Sender"" text,
						""Host"" text,
						""UserName"" text,
						""Password"" text
					);";
        }

        public static string DropTableScript()
        {
	        return @"drop table if exists ""TestType14s"";";
        }
    }
}