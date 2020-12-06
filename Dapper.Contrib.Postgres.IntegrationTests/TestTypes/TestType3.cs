using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType3
    {
        [Column("MyId")]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Column("MyBalance")]
        public decimal Balance { get; set; }
    }
}