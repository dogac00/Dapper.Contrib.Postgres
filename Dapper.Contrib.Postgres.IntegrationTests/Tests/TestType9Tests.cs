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
    public class TestType9Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType9()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenKey = Fixture.Create<string>();
            var item = Fixture
                .Build<TestType9>()
                .With(i => i.FirstName, givenKey)
                .Create();

            await conn.InsertAsync(item);

            item.FirstName.Should().Be(givenKey);

            var retrievedItem = await conn.QueryFirstAsync<TestType9>(
                @"select Name as FirstName, LastName
                      from TestType9");

            retrievedItem.Should().BeEquivalentTo(item);
        }
    }
}