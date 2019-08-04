using System.Collections.Generic;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using Xunit.UserContext.XunitExtensions;

namespace Xunit.UserContext.Tests.xUnitExtensions
{
    public class UserTheoryDiscovererTests
    {
        private readonly IMessageSink subMessageSink;

        public UserTheoryDiscovererTests()
        {
            subMessageSink = Substitute.For<IMessageSink>();
        }

        private UserTheoryDiscoverer CreateUserTheoryDiscoverer()
        {
            return new UserTheoryDiscoverer(this.subMessageSink);
        }

        [Fact]
        public void Discover_WithNullAttribute_CreatesTestCase()
        {
            // Arrange
            var userTheoryDiscoverer = this.CreateUserTheoryDiscoverer();
            var discoveryOptions = TestFrameworkOptions.ForDiscovery();
            var testMethod = Substitute.For<ITestMethod>();
            var theoryAttribute = Substitute.For<IAttributeInfo>();            

            // Act
            var result = userTheoryDiscoverer.Discover(discoveryOptions, testMethod, theoryAttribute);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserContextTestCase>>(result);
        }
    }
}
