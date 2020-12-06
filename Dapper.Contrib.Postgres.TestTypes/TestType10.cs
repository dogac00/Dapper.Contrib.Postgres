using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("MyTestTable")]
    public class TestType10
    {
        [Column("MyIntField")]
        public int IntField { get; set; }
        [Column("MyDateTimeField")]
        public DateTime DateTimeField { get; set; }
    }
}