using Dapper.Contrib.Postgres.Helpers;
using Dapper.Contrib.Postgres.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    public class UseQuotedIdentifiersAttributeTests
    {
        [Test]
        public void ShouldGetTableName_WithQuotedIdentifiersAttribute()
        {
            var tableName = Extensions.GetTableName<TestClassWithQuotedIdentifiersAttribute>();

            var expectedTableName = "TestClassWithQuotedIdentifiersAttributes".AddQuotes();

            tableName.Should().Be(expectedTableName);
        }
        
        [Test]
        public void ShouldGetColumnNames_WithQuotedIdentifiersAndTableAttribute()
        {
            var tableName = Extensions.GetTableName<TestClassWithQuotedIdentifiersAndTableAttribute>();

            var expectedTableName = "MyTable";

            tableName.Should().Be(expectedTableName);
        }
        
        [Test]
        public void ShouldGetColumnNamesWithQuotedIdentifiersAndColumnAttribute()
        {
            var columnNames = Extensions.GetColumnNames<TestClassWithQuotedIdentifiersAndTableAttribute>();
            
            var expectedTableName1 = "FirstNameColumn";
            var expectedTableName2 = "SecondNameColumn";

            columnNames.Should().Contain(expectedTableName1);
            columnNames.Should().Contain(expectedTableName2);
        }
    }
}