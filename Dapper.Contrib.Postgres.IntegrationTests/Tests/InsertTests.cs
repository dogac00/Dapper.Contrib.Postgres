using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class InsertTests : IntegrationTestBase
    {
        private IFixture _fixture;
        private IDbConnectionFactory _connectionFactory;

        [SetUp]
        public async Task SetUp()
        {
            _fixture = new Fixture();
            _connectionFactory = GetRequiredService<IDbConnectionFactory>();

            var connection = _connectionFactory.CreateConnection(null);

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""Employee1s""
                                              (
                                                ""Id"" primary key not null,
                                                ""Name"" text
                                              )");
        }

        [TearDown]
        public async Task TearDown()
        {
            _connectionFactory = GetRequiredService<IDbConnectionFactory>();

            var connection = _connectionFactory.CreateConnection(null);
            
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employee1s""");
        }

        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeWithUseQuotedIdentifiersAttribute()
        {
            var connection = _connectionFactory.CreateConnection(null);
            
            var employeeGiven = _fixture.Create<Employee1>();

            var sql = QueryHelper.GetInsertSqlForSqLite<Employee1>();

            await connection.ExecuteAsync(sql, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee1>(@"SELECT * FROM ""Employee1s""");

            var employeeList = employees as List<Employee1> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved!.Id.Should().Be(employeeGiven.Id);
            employeeRetrieved.Name.Should().Be(employeeGiven.Name);
        }
    }
}