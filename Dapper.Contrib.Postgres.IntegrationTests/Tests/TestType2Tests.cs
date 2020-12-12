using System;
using System.Linq;
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
    public class TestType2Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType2()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();
            var initialId = Guid.NewGuid().ToString();
            var item = new TestType2
            {
                Id = initialId,
                Name = Guid.NewGuid().ToString()
            };

            await connection.InsertAsync(item);

            item.Id.Should().Be(initialId);

            var retrievedItems = (await connection
                    .QueryAsync<TestType2>("select * from TestType2s"))
                .ToList();

            retrievedItems.Count.Should().Be(1);
            var retrievedItem = retrievedItems.First();

            retrievedItem.Id.Should().Be(item.Id);
            retrievedItem.Name.Should().Be(item.Name);
        }

        [Test]
        public async Task ShouldInsertTestType2_MultipleTimes()
        {
            var fixture = new Fixture();
            var items = fixture
                .CreateMany<TestType2>()
                .ToList();

            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            foreach (var item in items)
            {
                await conn.InsertAsync(item);
            }

            var retrievedItems = await conn.GetAll<TestType2>();

            retrievedItems.Should().BeEquivalentTo(items);
        }
    }
}