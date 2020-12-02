using System;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests
{
    public class ExtensionsTests
    {
        private IFixture _fixture;
        
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void ShouldGetTableNameWithAttributeDefined()
        {
            var tableName = Extensions.GetTableName<TestClassWithTableAttribute>();

            tableName.Should().Be("TestTable");
        }
    }
}