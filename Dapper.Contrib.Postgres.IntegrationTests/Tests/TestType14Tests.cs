using System;
using System.Collections.Generic;
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
    public class TestType14Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType13()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenId = Fixture.Create<long>();
            var item = Fixture.Build<TestType14>()
                .With(i => i.Id, givenId)
                .Create();

            await conn.InsertAsync(item);

            item.Id.Should().NotBe(givenId);
            item.Id.Should().Be(1);

            var retrievedItem = (await conn.GetAll<TestType14>())
                .First();

            retrievedItem.Should().BeEquivalentTo(item);
        }

        [Test]
        public async Task ShouldGenerateSequentialIds()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var index = 1;
            var givenCount = new Random().Next(250);

            for (var i = 0; i < givenCount; i++)
            {
                var item = Fixture.Create<TestType14>();
                await conn.InsertAsync(item);

                item.Id.Should().Be(index++);
            }

            var count = await conn.QueryFirstAsync<int>(@"select count(*) from ""TestType14s"";");

            count.Should().Be(givenCount);
        }
        
        [Test]
        public async Task ShouldInsertAll()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();
            var count = new Random().Next(500);
            var givenItems = new List<TestType14>();

            for (var i = 0; i < count; i++)
            {
                var item = Fixture.Create<TestType14>();
                givenItems.Add(item);
                await conn.InsertAsync(item);
            }

            var retrievedItems = await conn.GetAll<TestType14>();

            retrievedItems.Should().HaveCount(count);

            for (var i = 0; i < count; i++)
            {
                var retrievedItem = retrievedItems[i];
                var item = givenItems[i];
                retrievedItem.Should().BeEquivalentTo(item);
            }
        }
    }
}