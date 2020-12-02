using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.TestTypes
{
    public class TestClassWithKeyAttribute
    {
        [Key]
        public string MyKey { get; set; }
    }
}