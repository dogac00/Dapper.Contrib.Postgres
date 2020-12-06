using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType15
    {
        public long Id { get; set; }
        public long AnotherId { get; set; }
        public string Data { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}