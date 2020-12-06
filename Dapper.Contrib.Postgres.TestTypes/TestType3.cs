using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    public class TestType3
    {
        [Column("MyId")]
        public long Id { get; set; }
        public string Name { get; set; }
        [Column("MyDate")]
        public DateTime Date { get; set; }
        public decimal Money { get; set; }
    }
}