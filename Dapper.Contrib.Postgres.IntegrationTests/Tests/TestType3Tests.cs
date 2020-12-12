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
    public class TestType3Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsert_TestType3()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();
            var item = Fixture
                .Create<TestType3>();

            await connection.InsertAsync(item);

            var retrievedItems = await connection.GetAll<TestType3>();

            retrievedItems.Should().HaveCount(1);
            var retrievedItem = retrievedItems.First();

            retrievedItem.Name.Should().Be(item.Name);
            retrievedItem.Balance.Should().Be(item.Balance);
            retrievedItem.Date.Should().BeSameDateAs(item.Date);
        }
    }
}