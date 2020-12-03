using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.TestTypes
{
    [UseQuotedIdentifiers]
    [Table("MyTestTable")]
    public class TestClassWithQuoteTableAndColumnAttribute
    {
        [Column("MyIntField")]
        public int IntField { get; set; }
        [Column("MyDateTimeField")]
        public DateTime DateTimeField { get; set; }
    }
}