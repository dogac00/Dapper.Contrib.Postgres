using System.Collections.Generic;
using Dapper.Contrib.Postgres.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Dapper.Contrib.Postgres.Tests.Tests
{
    public class ExtensionsTests
    {
        [Test]
        public void ShouldGetParameters_WhenTestClassWithPropertiesIsGiven()
        {
            var parameters = Extensions.GetParameters<TestClassWithProperties>();

            var expected = new List<string>
            {
                "@LongField", "@StringField"
            };

            parameters.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ShouldGetParameters_WhenTestClassWithoutPropertiesIsGiven()
        {
            var parameters = Extensions.GetParameters<TestClassWithoutProperties>();

            parameters.Should().BeEquivalentTo(new List<string>());
        }
    }
}