using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Helpers;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using Npgsql;
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

            _connectionFactory = GetRequiredService<IDbConnectionFactory>();

            var connection = _connectionFactory.CreateConnection(null);

            await DatabaseHelper.DropTables(connection);
            
            await DatabaseHelper.CreateTables(connection);
        }
        
        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await DatabaseHelper.DropTables(_connectionFactory.CreateConnection(null));
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

        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeClassWithOnlyQuoteAttribute()
        {
            var connection = _connectionFactory.CreateConnection(null);

            var employeeGiven = _fixture.Create<Employee6Type>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee6Type>(@"SELECT *
                                                             FROM ""Employee6Types""");

            var employeeList = employees as List<Employee6Type> ?? employees.ToList();

            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
        }

        [Test]
        public async Task ShouldInsert_UnquotedEmployee()
        {
            var connection = _connectionFactory.CreateConnection(null);

            var employeeGiven = _fixture.Create<UnquotedEmployee>();

            if (connection is NpgsqlConnection)
            {
                Assert.ThrowsAsync<PostgresException>(async () =>
                {
                    await DatabaseHelper.InsertGeneric(connection, employeeGiven);

                    _ =
                        await connection.QueryAsync<UnquotedEmployee>(@"SELECT *
                                                                    FROM ""UnquotedEmployees""");
                });
            }
            else
            {
                await DatabaseHelper.InsertGeneric(connection, employeeGiven);

                var employees =
                    await connection.QueryAsync<UnquotedEmployee>(@"SELECT *
                                                                    FROM ""UnquotedEmployees""");

                var employeeList = employees as List<UnquotedEmployee> ?? employees.ToList();

                employeeList.Count.Should().Be(1);
                var employeeRetrieved = employeeList.FirstOrDefault();
                employeeRetrieved.Should().NotBeNull();
                employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
            }
        }

        [Test]
        public async Task ShouldInsert_QuotedEmployee()
        {
            var connection = _connectionFactory.CreateConnection(null);

            var employeeGiven = _fixture.Create<QuotedEmployee>();

            if (connection is NpgsqlConnection)
            {
                Assert.ThrowsAsync<PostgresException>(async () =>
                {
                    await DatabaseHelper.InsertGeneric(connection, employeeGiven);

                    _ =
                        await connection.QueryAsync<QuotedEmployee>(@"SELECT *
                                                                  FROM ""QuotedEmployees""");
                });
            }
            else
            {
                await DatabaseHelper.InsertGeneric(connection, employeeGiven);

                var employees =
                    await connection.QueryAsync<QuotedEmployee>(@"SELECT *
                                                                  FROM ""QuotedEmployees""");

                var employeeList = employees as List<QuotedEmployee> ?? employees.ToList();

                employeeList.Count.Should().Be(1);
                var employeeRetrieved = employeeList.FirstOrDefault();
                employeeRetrieved.Should().NotBeNull();
                employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
            }
        }

        [Test]
        public async Task ShouldInsert_WhenEmployeeWithDateGiven()
        {
            var connection = _connectionFactory.CreateConnection(null);

            var employeeGiven = _fixture.Create<EmployeeWithDate>();

            await DatabaseHelper.InsertGeneric(connection, employeeGiven);

            var employees =
                await connection.QueryAsync<EmployeeWithDate>(@"SELECT *
                                                                  FROM EmployeeWithDates");

            var employeeList = employees as List<EmployeeWithDate> ?? employees.ToList();
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved!.Id.Should().Be(employeeGiven.Id);
            employeeRetrieved.BirthDate.Should().BeSameDateAs(employeeGiven.BirthDate);
        }
    }
}