using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class TestType4Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsert()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();
            
            var item = Fixture.Create<TestType4>();

            await conn.InsertAsync(item);

            var retrievedItem = await conn.QueryFirstAsync<TestType4>(
                @"select BirthDate as BirthDateColumn 
                      from TestType4s");

            retrievedItem.BirthDateColumn.Should().BeSameDateAs(item.BirthDateColumn);
        }
    }
}