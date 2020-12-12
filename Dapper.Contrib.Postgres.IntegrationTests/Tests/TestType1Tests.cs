using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class TestType1Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType1()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenId = Fixture.Create<long>();
            var givenItem = new TestType1
            {
                Id = givenId,
                Name = Fixture.Create<string>()
            };

            await connection.InsertAsync(givenItem);

            var items = (await connection
                .QueryAsync<TestType1>(@"SELECT * FROM TestType1s"))
                .ToList();

            items.Count.Should().Be(1);
            var retrievedItem = items.First();

            givenItem.Id.Should().Be(1);
            retrievedItem.Id.Should().NotBe(givenId);
            retrievedItem.Id.Should().Be(1);
            retrievedItem.Name.Should().Be(givenItem.Name);
        }
    }
}