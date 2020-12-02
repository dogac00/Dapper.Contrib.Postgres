using System.Linq;
using System.Reflection;
using Dapper.Contrib.Postgres.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    public class ColumnAttributeTests
    {
        [Test]
        public void ShouldGetColumnName_WhenColumnAttributeIsDefined()
        {
            var propertyInfo = typeof(TestClassWithColumnAttribute)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .First();
            var columnName = Extensions.GetColumnName<TestClassWithColumnAttribute>(propertyInfo);

            columnName.Should().Be("BirthDate");
        }
        
        [Test]
        public void ShouldGetColumnName_WhenColumnAttributeIsNotDefined()
        {
            var propertyInfo = typeof(TestClassWithoutColumnAttribute)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .First();
            var columnName = Extensions.GetColumnName<TestClassWithColumnAttribute>(propertyInfo);

            columnName.Should().Be("BirthDateColumn");
        }
        
        [Test]
        public void ShouldGetColumns_WhenColumnAttributesAreDefined()
        {
            var names = Extensions.GetColumnNames<TestClassWithMultipleProperties>();

            names.Contains("MyId").Should().BeTrue();
            names.Contains("MyDate").Should().BeTrue();
            names.Contains("Name").Should().BeTrue();
            names.Contains("Money").Should().BeTrue();
        }
        
        [Test]
        public void ShouldGetTableName_WithPluralIdentifier()
        {
            var expected = nameof(TestClassWithMultipleProperties);
            var tableName = Extensions.GetTableName<TestClassWithMultipleProperties>();

            expected.Should().Be(tableName);
        }
        
        [Test]
        public void ShouldNotGetTableName_WithQuotedAttributes()
        {
            var expected = nameof(TestClassWithQuotedIdentifiersAttribute) + "s";
            var tableName = Extensions.GetTableName<TestClassWithMultipleProperties>();

            expected.Should().NotBe(tableName);
        }
        
        [Test]
        public void ShouldGetTableName_WithQuotedAttributes()
        {
            var expected = "MyTable";
            var tableName = Extensions.GetTableName<TestClassWithQuotedIdentifiersAndTableAttribute>();

            expected.Should().Be(tableName);
        }
    }
}