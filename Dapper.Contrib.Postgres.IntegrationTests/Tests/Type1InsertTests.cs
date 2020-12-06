using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.Helpers;
using Dapper.Contrib.Postgres.IntegrationTests.Template;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Tests
{
    public class Type1InsertTests : IntegrationTestBase
    {
        private IDbConnectionFactory _connectionFactory;
        private IFixture _fixture;

        [OneTimeSetUp]
        public override async Task OneTimeSetUp()
        {
            await base.OneTimeSetUp();

            _connectionFactory = GetRequiredService<IDbConnectionFactory>();
            
            var connection = _connectionFactory.CreateConnection();

            await connection.DropTable("\"TestType1s\"");
            
            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS ""TestType1s""
                                                  (
                                                    ""Id"" bigserial not null
                                                        constraint testtype1_pk
                                                        primary key,
                                                    ""RelatedId"" bigint,
                                                    ""UserId"" bigint
                                                  );");
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var connection = _connectionFactory.CreateConnection();
            
            await connection.DropTable("\"TestType1s\"");
        }

        [SetUp]
        public void SetUp()
        {
            _connectionFactory = GetRequiredService<IDbConnectionFactory>();
            _fixture = new Fixture();
        }
        
        [Test]
        public async Task ShouldInsertType1()
        {
            var conn = _connectionFactory.CreateConnection();
            var givenEntity = _fixture.Build<TestType1>()
                .Create();

            var insertedId = await conn.InsertAsync(givenEntity);

            insertedId.Should().Be(1);

            var retrievedItems = (await conn.QueryAsync<TestType1>(@"SELECT * FROM ""TestType1s"""))
                .ToList();
            
            retrievedItems.Count.Should().Be(1);
            var retrievedEntity = retrievedItems.FirstOrDefault();

            retrievedEntity!.RelatedId.Should().Be(givenEntity.RelatedId);
            retrievedEntity!.UserId.Should().Be(givenEntity.UserId);
        }
        
        [Test]
        public async Task ShouldInsertType1_MultipleTimes()
        {
            var conn = _connectionFactory.CreateConnection();
            var count = _fixture.Create<int>();
            var givenEntities = _fixture.Build<TestType1>()
                .CreateMany(count)
                .ToList();

            var shouldId = 2;
            foreach (var givenEntity in givenEntities)
            {
                var id = await conn.InsertAsync(givenEntity);

                if (id is long longId)
                {
                    longId.Should().Be(shouldId);
                    shouldId++;
                }
                else
                {
                    Assert.Fail();
                }
            }

            var retrievedEntities = (await conn.QueryAsync<TestType1>(@"SELECT * FROM ""TestType1s"""))
                .ToList();
            
            retrievedEntities.Count.Should().Be(count);

            for (var i = 0; i < count; i++)
            {
                retrievedEntities[i].RelatedId.Should().Be(givenEntities[i].RelatedId);
                retrievedEntities[i].UserId.Should().Be(givenEntities[i].UserId);
            }
        }
    }
}