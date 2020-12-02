using System.Collections.Generic;
using Dapper.Contrib.Postgres.Helpers;
using Dapper.Contrib.Postgres.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    public class ExtensionsTests
    {
        [Test]
        public void ShouldGetParameters_WhenTestClassWithPropertiesIsGiven()
        {
            var parameters = Extensions.GetParameters<TestClassWithProperties>();

            var expected = new List<string>
            {
                "@LongField", "@StringField"
            };

            parameters.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ShouldGetParameters_WhenTestClassWithoutPropertiesIsGiven()
        {
            var parameters = Extensions.GetParameters<TestClassWithoutProperties>();

            parameters.Should().BeEquivalentTo(new List<string>());
        }
        
        [Test]
        public void ShouldGetColumnNames_WhenTestClassWithPropertiesIsGiven()
        {
            var columnNames = Extensions.GetColumnNames<TestClassWithProperties>();

            var expectedColumnNames = new List<string>
            {
                "LongField",
                "StringField"
            };
            
            columnNames.Should().BeEquivalentTo(expectedColumnNames);
        }
        
        [Test]
        public void ShouldGetColumnNames_WhenTestClassWithoutPropertiesIsGiven()
        {
            var columnNames = Extensions.GetColumnNames<TestClassWithoutProperties>();

            columnNames.Should().BeEquivalentTo(new List<string>());
        }
        
        [Test]
        public void ShouldGetColumnNames_WhenBothQuoteTableAndColumnAttributeIsDefined()
        {
            var columnNames = Extensions.GetColumnNames<TestClassWithQuoteTableAndColumnAttribute>();
            var expectedColumnNames = new List<string>()
            {
                "MyIntField",
                "MyDateTimeField"
            };
            
            columnNames.Should().BeEquivalentTo(expectedColumnNames);
        }
        
        [Test]
        public void ShouldGetTableName_WhenBothQuoteTableAndColumnAttributeIsDefined()
        {
            var columnNames = Extensions.GetTableName<TestClassWithQuoteTableAndColumnAttribute>();
            var expectedTableName = "MyTestTable";
            
            columnNames.Should().Be(expectedTableName);
        }
        
        [Test]
        public void ShouldGetParameters_WhenBothQuoteTableAndColumnAttributeIsDefined()
        {
            var parameters = Extensions.GetParameters<TestClassWithQuoteTableAndColumnAttribute>();
            var expectedParameters = new List<string>()
            {
                "@IntField",
                "@DateTimeField"
            };
            
            parameters.Should().BeEquivalentTo(expectedParameters);
        }
        
        [Test]
        public void ShouldGetInsertSql_WhenBothQuoteTableAndColumnAttributeIsDefined()
        {
            var sql = Extensions.GetInsertSql<TestClassWithQuoteTableAndColumnAttribute>();

            var expected = @"INSERT INTO MyTestTable 
                             (MyIntField, MyDateTimeField) 
                             VALUES (@IntField, @DateTimeField);";
            
            sql.RemoveWhitespace().Should().Be(expected.RemoveWhitespace());
        }
        
        [Test]
        public void ShouldGetInsertSql_WhenTestClassWithPropertiesGiven()
        {
            var sql = Extensions.GetInsertSql<TestClassWithProperties>();

            var expected = @"INSERT INTO TestClassWithProperties 
                             (LongField, StringField) 
                             VALUES (@LongField, @StringField);";
            
            sql.RemoveWhitespace().Should().Be(expected.RemoveWhitespace());
        }
        
        [Test]
        public void ShouldGetInsertSql_WhenTestClassWithColumnAttributesGiven()
        {
            var sql = Extensions.GetInsertSql<TestClassWithColumnAttribute>();

            var expected = @"INSERT INTO TestClassWithColumnAttributes 
                             (BirthDate) 
                             VALUES (@MyDateTimeColumn);";
            
            sql.RemoveWhitespace().Should().Be(expected.RemoveWhitespace());
        }
        
        [Test]
        public void ShouldGetInsertSql_WhenTestClassWithQuotedIdentifiersAttributeGiven()
        {
            var sql = Extensions.GetInsertSql<TestClassWithQuotedIdentifiersAttribute>();

            var expected = @"INSERT INTO ""TestClassWithQuotedIdentifiersAttributes"" 
                             (""FirstName"", ""LastName"") 
                             VALUES (@FirstName, @LastName);";
            
            sql.RemoveWhitespace().Should().Be(expected.RemoveWhitespace());
        }
    }
}