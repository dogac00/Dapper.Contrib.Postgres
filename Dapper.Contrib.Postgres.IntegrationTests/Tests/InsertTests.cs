using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.Json;
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
        }

        [TearDown]
        public async Task TearDown()
        {
            _connectionFactory = GetRequiredService<IDbConnectionFactory>();

            var connection = _connectionFactory.CreateConnection(null);
            
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS ""Employee1s""");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS MyEmployees");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS EmployeeTable");
            await connection.ExecuteAsync(@"DROP TABLE IF EXISTS Employee4s");
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
        
        [Test]
        public async Task ShouldInsert_WhenGivenEmployeeWithoutUseQuotedIdentifiersAttribute()
        {
            var connection = _connectionFactory.CreateConnection(null);
            
            var employeeGiven = _fixture.Create<Employee2>();

            var sql = QueryHelper.GetInsertSqlForSqLite<Employee2>();

            await connection.ExecuteAsync(sql, employeeGiven);

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

            var sql = QueryHelper.GetInsertSqlForSqLite<Employee3>();

            await connection.ExecuteAsync(sql, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee3>(@"SELECT ""EmployeeId"", ""EmployeeName"" as Name 
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

            var sql = QueryHelper.GetInsertSqlForSqLite<Employee4>();

            await connection.ExecuteAsync(sql, employeeGiven);

            var employees =
                await connection.QueryAsync<Employee4>(@"SELECT *
                                                             FROM Employee4s");

            var employeeList = employees as List<Employee4> ?? employees.ToList();
            
            employeeList.Count.Should().Be(1);
            var employeeRetrieved = employeeList.FirstOrDefault();
            employeeRetrieved.Should().NotBeNull();
            employeeRetrieved.JsonEquals(employeeGiven).Should().BeTrue();
        }
    }
}