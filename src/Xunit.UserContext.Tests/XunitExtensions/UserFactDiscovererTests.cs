using NSubstitute;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.UserContext.XunitExtensions;

namespace xUnit.UserContext.Tests.xUnitExtensions
{
    public class UserFactDiscovererTests
    {
        private readonly IMessageSink _subMessageSink;

        public UserFactDiscovererTests()
        {
            this._subMessageSink = Substitute.For<IMessageSink>();
        }

        private UserFactDiscoverer CreateUserFactDiscoverer()
        {
            return new UserFactDiscoverer(_subMessageSink);
        }

        [Fact]
        public void Discover_WithNullAttribute_CreatesTestCase()
        {
            // Arrange
            var userFactDiscoverer = this.CreateUserFactDiscoverer();
            var discoveryOptions = TestFrameworkOptions.ForDiscovery();
            var testMethod = Substitute.For<ITestMethod>();
            IAttributeInfo factAttribute = null;

            // Act
            var result = userFactDiscoverer.Discover(discoveryOptions, testMethod, factAttribute);

            // Assert
            Assert.IsAssignableFrom<UserContextTestCase>(result.Single());
        }
    }
}
