using System;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class TestType12Tests : DatabaseTest
    {
        [Test]
        public void ShouldThrowExceptionTestType12()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            Assert.Throws<InvalidOperationException>(() =>
            {
                conn.Insert(Fixture.Create<TestType12>());
            });
        }
        
        [Test]
        public void ShouldThrowExceptionTestType12_Async()
        {
            var conn = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await conn.InsertAsync(Fixture.Create<TestType12>());
            });
        }
    }
}