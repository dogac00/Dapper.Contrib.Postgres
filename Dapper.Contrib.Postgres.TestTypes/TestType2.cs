using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    public class TestType2
    {
        [Key]
        public string MyKey { get; set; }
    }
}