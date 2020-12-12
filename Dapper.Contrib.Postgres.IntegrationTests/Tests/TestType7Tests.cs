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
    public class TestType7Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsert()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenId = Fixture.Create<long>();
            var item = Fixture
                .Build<TestType7>()
                .With(i => i.LongField, givenId)
                .Create();

            await conn.InsertAsync(item);

            var retrievedItem = await conn.QueryFirstAsync<TestType7>(
                @"select LongField, MyStringField as StringField
                      from MyTestType7");

            item.LongField.Should().NotBe(givenId);
            item.LongField.Should().Be(1);
            retrievedItem.StringField.Should().Be(item.StringField);
        }
    }
}