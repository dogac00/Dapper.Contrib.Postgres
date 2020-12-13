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
    public class TestType13Tests : DatabaseTest
    {
        [Test]
        public async Task ShouldInsertTestType13()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var givenId = Fixture.Create<long>();
            var item = Fixture.Build<TestType13>()
                .With(i => i.Id, givenId)
                .Create();

            await conn.InsertAsync(item);

            item.Id.Should().NotBe(givenId);
            item.Id.Should().Be(1);

            var retrievedItem = (await conn.GetAll<TestType13>())
                .First();

            retrievedItem.Id.Should().Be(1);
            retrievedItem.RelatedId.Should().Be(item.RelatedId);
            retrievedItem.UserId.Should().Be(item.UserId);
            retrievedItem.UserEmail.Should().Be(item.UserEmail);
            retrievedItem.Date.Should().BeSameDateAs(item.Date);
            retrievedItem.Type.Should().Be(item.Type);
        }

        [Test]
        public async Task ShouldGenerateSequentialIds()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var index = 1;

            for (var i = 0; i < 250; i++)
            {
                var item = Fixture.Create<TestType13>();
                await conn.InsertAsync(item);

                item.Id.Should().Be(index++);
            }

            var count = await conn.QueryFirstAsync<int>(@"select count(*) from ""TestType13s"";");

            count.Should().Be(250);
        }
        
        [Test]
        public async Task ShouldInsertAll()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();
            var count = new Random().Next(2500);
            var givenItems = new List<TestType13>();

            for (var i = 0; i < count; i++)
            {
                var item = Fixture.Create<TestType13>();
                givenItems.Add(item);
                await conn.InsertAsync(item);
            }

            var retrievedItems = await conn.GetAll<TestType13>();

            retrievedItems.Should().HaveCount(count);

            for (int i = 0; i < count; i++)
            {
                var retrievedItem = retrievedItems[i];
                var item = givenItems[i];
                retrievedItem.Id.Should().Be(i + 1);
                retrievedItem.RelatedId.Should().Be(item.RelatedId);
                retrievedItem.UserId.Should().Be(item.UserId);
                retrievedItem.UserEmail.Should().Be(item.UserEmail);
                retrievedItem.Date.Should().BeSameDateAs(item.Date);
                retrievedItem.Type.Should().Be(item.Type);
            }
        }
    }
}