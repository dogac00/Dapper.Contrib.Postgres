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
            var columnName = Extensions.GetColumnName(propertyInfo);

            columnName.Should().Be("BirthDate");
        }
        
        [Test]
        public void ShouldGetColumnName_WhenColumnAttributeIsNotDefined()
        {
            var propertyInfo = typeof(TestClassWithoutColumnAttribute)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .First();
            var columnName = Extensions.GetColumnName(propertyInfo);

            columnName.Should().Be("\"BirthDateColumn\"");
        }
    }
}