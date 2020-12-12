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
    public class TestType6Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsert()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenId = Fixture.Create<int>();
            var item = Fixture
                .Build<TestType6>()
                .With(i => i.Id, givenId)
                .Create();

            await conn.InsertAsync(item);

            var retrievedItems = await conn.GetAll<TestType6>();

            retrievedItems.Should().HaveCount(1);
            retrievedItems.Should().BeEquivalentTo(item);
        }
    }
}