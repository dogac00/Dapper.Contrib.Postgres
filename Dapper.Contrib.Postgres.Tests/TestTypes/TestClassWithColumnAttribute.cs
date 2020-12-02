using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.Tests.TestTypes
{
    public class TestClassWithColumnAttribute
    {
        [Column("BirthDate")]
        public DateTime MyDateTimeColumn { get; set; }
    }
}