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
    public class TestType11Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType11()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenId = Fixture.Create<int>();
            var item = new TestType11
            {
                Id = givenId,
                Data = Fixture.Create<string>()
            };

            await conn.InsertAsync(item);

            item.Id.Should().Be(givenId);

            var retrievedItem = (await conn
                .GetAll<TestType11>())
                .First();

            retrievedItem.Should().BeEquivalentTo(item);
        }
    }
}