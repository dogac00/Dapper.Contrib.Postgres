using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class TestType5Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsert()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenData = Fixture.Create<string>();
            var item = new TestType5()
            {
                Data = givenData
            };

            await conn.InsertAsync(item);

            var retrievedItem = await conn.QueryFirstAsync<TestType5>(
                @"select MyData as Data 
                      from TestType5");

            item.Data.Should().Be(givenData);
            retrievedItem.Data.Should().Be(item.Data);
        }
    }
}