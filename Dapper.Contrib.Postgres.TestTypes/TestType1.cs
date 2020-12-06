using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    public class TestType1
    {
        [Column("BirthDate")]
        public DateTime MyDateTimeColumn { get; set; }
    }
}