using Dapper.Contrib.Postgres.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    public class TableAttributeTests
    {
        [Test]
        public void ShouldGetTableName_WhenAttributeDefined()
        {
            var tableName = Extensions.GetTableName<TestClassWithTableAttribute>();

            tableName.Should().Be("TestTable");
        }
        
        [Test]
        public void ShouldGetTableName_WhenAttributeIsNotDefined()
        {
            var nameOfType = nameof(TestClassWithoutTableAttribute);
            var tableName = Extensions.GetTableName<TestClassWithoutTableAttribute>();

            tableName.Should().Be(nameOfType + "s");
        }
    }
}