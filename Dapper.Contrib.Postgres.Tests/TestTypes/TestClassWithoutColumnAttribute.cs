using System;

namespace Dapper.Contrib.Postgres.Tests.TestTypes
{
    public class TestClassWithoutColumnAttribute
    {
        public DateTime BirthDateColumn { get; set; }
    }
}