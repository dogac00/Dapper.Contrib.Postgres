using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Helpers;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class InsertTests : IntegrationTestBase
    {
        private IFixture _fixture;
        private IDbConnectionFactory _connectionFactory;

        [OneTimeSetUp]
        public override async Task OneTimeSetUp()
        {
            await base.OneTimeSetUp();

            await DropTables();
            
            await CreateTables();
        }
        
        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await DropTables();
        }

        private async Task CreateTables()
        {
            _connectionFactory = GetRequiredService<IDbConnectionFactory>();

            var connection = _connectionFactory.CreateConnection(null);

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""Employees""
                                              (
                                                ""Id"" bigint primary key not null,
                                                ""Name"" text
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS MyEmployees
                                              (
                                                MyFirstName text,
                                                MyLastName text
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS EmployeeTable
                                              (
                                                ""EmployeeId"" bigint,
                                                EmployeeName text
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Employee4s
                                              (
                                                Id bigint,
                                                Name text,
                                                DateUnixTimestamp bigint
                                              )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""Employee5""
                                              (
                                                ""Id"" bigint,
                                                ""Name"" text,
                                                ""Money"" numeric
                                              )");
        }

        private async Task DropTables()
        {
            _connectionFactory = GetRequiredService<IDbConnectionFactory>();

            var connection = _connectionFactory.CreateConnection(null);

            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employee1s""");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS MyEmployees");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS EmployeeTable");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS Employee4s");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employee5""");
        }

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeWithUseQuotedIdentifiersAttribute()
        {
            var connection = _connectionFactory.CreateConnection(null);
            var employeeGiven = _fixture.Create<Employee>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee>(@"SELECT * FROM ""Employees""");

            var employeeList = employees as List<Employee> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved!.Id.Should().Be(employeeGiven.Id);
            employeeRetrieved.Name.Should().Be(employeeGiven.Name);
        }
        
        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeWithoutUseQuotedIdentifiersAttribute()
        {
            var connection = _connectionFactory.CreateConnection(null);
            
            var employeeGiven = _fixture.Create<Employee2>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee2>(@"SELECT MyFirstName as FirstName,
                                                            MyLastName as LastName
                                                            FROM MyEmployees");

            var employeeList = employees as List<Employee2> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
        }
        
        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeWithKeyAttribute()
        {
            var connection = _connectionFactory.CreateConnection(null);
            
            var employeeGiven = _fixture.Create<Employee3>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee3>(@"SELECT ""EmployeeId"", EmployeeName as Name 
                                                             FROM EmployeeTable");

            var employeeList = employees as List<Employee3> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
        }
        
        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeClassWithNoAttributes()
        {
            var connection = _connectionFactory.CreateConnection(null);
            
            var employeeGiven = _fixture.Create<Employee4>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee4>(@"SELECT *
                                                             FROM Employee4s");

            var employeeList = employees as List<Employee4> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
        }
        
        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeClassWithTableAndQuoteAttributes()
        {
            var connection = _connectionFactory.CreateConnection(null);
            
            var employeeGiven = _fixture.Create<Employee5>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee5>(@"SELECT *
                                                             FROM ""Employee5""");

            var employeeList = employees as List<Employee5> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
        }
    }
}