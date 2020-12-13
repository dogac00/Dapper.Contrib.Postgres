using System;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class TestType8Tests : DatabaseTest
    {
        [Test]
        public void ShouldThrowExceptionAsync()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var item = Fixture.Create<TestType8>();
            
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await connection.InsertAsync(item);
            });
        }
        
        [Test]
        public void ShouldThrowException()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            var item = Fixture.Create<TestType8>();
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                connection.Insert(item);
            });
        }
    }
}