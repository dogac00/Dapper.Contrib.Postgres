using System.Threading.Tasks;
using AutoFixture;
using Dapper.Contrib.Postgres.IntegrationTests.Connection;
using Dapper.Contrib.Postgres.IntegrationTests.TestTypes;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.IntegrationTests.Template
{
    public class DatabaseTest : IntegrationTestBase
    {
        protected readonly IFixture Fixture = new Fixture();
        
        [SetUp]
        public virtual async Task SetUp()
        {
            await DatabaseSetUp();
        }

        [TearDown]
        public virtual async Task TearDown()
        {
            await DatabaseTeardown();
        }

        private async Task DatabaseSetUp()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            await connection.ExecuteAsync(TestType1.CreateTableScript());
            await connection.ExecuteAsync(TestType2.CreateTableScript());
            await connection.ExecuteAsync(TestType3.CreateTableScript());
            await connection.ExecuteAsync(TestType4.CreateTableScript());
            await connection.ExecuteAsync(TestType5.CreateTableScript());
            await connection.ExecuteAsync(TestType6.CreateTableScript());
            await connection.ExecuteAsync(TestType7.CreateTableScript());
        }

        private async Task DatabaseTeardown()
        {
            var connection = GetRequiredService<IDbConnectionFactory>()
                .CreateConnection();

            await connection.ExecuteAsync(TestType1.DropTableScript());
            await connection.ExecuteAsync(TestType2.DropTableScript());
            await connection.ExecuteAsync(TestType3.DropTableScript());
            await connection.ExecuteAsync(TestType4.DropTableScript());
            await connection.ExecuteAsync(TestType5.DropTableScript());
            await connection.ExecuteAsync(TestType6.DropTableScript());
            await connection.ExecuteAsync(TestType7.DropTableScript());
        }
    }
}