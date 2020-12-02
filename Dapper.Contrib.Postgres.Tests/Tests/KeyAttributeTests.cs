using Dapper.Contrib.Postgres.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    public class KeyAttributeTests
    {
        [Test]
        public void ShouldGetKeyProperty_WhenTestClassWithKeyAttributeGiven()
        {
            var keyName = Extensions.GetKeyName<TestClassWithKeyAttribute>();

            keyName.Should().Be("MyKey");
        }
        
        [Test]
        public void ShouldGetKeyProperty_WhenTestClassWithoutKeyAttributeGiven()
        {
            var keyName = Extensions.GetKeyName<TestClassWithoutKeyAttributeWithColumnAttribute>();

            keyName.Should().Be("MyId");
        }
    }
}