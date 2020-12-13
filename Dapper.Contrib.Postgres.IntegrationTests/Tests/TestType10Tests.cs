using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Extensions;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class TestType10Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType10()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var item = Fixture.Create<TestType10>();

            await conn.InsertAsync(item);

            var retrievedItem = await conn.QueryFirstAsync<TestType10>(
                @"select ""MyIntField"" as Intfield,
                             ""MyDateTimeField"" as DateTimeField
                      from MyTestTable");

            retrievedItem.IntField.Should().Be(item.IntField);
            retrievedItem.DateTimeField.Should().BeSameDateAs(item.DateTimeField);
        }
    }
}