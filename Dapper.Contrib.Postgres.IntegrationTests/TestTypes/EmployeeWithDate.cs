using System;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    public class EmployeeWithDate
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
    }
}