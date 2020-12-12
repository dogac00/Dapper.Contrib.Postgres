using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
            
            var givenItem = new TestType1
            {
                Name = "employee name"
            };

            await connection.InsertAsync(givenItem);

            givenItem.Id.Should().Be(1);

            var items = (await connection
                .QueryAsync<TestType1>(@"SELECT * FROM Employees"))
                .ToList();

            items.Count.Should().Be(1);
            var retrievedItem = items.First();

            retrievedItem.Id.Should().Be(givenItem.Id);
            retrievedItem.Name.Should().Be(givenItem.Name);
        }
    }
}