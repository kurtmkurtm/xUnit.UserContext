using NSubstitute;
using System;
using Xunit;
using Xunit.UserContext;

namespace Xunit.UserContext.Tests
{
    public class UserFactAttributeTests
    {
        [Theory]
        [InlineData("Username", "Password", "Domain", true)]
        [InlineData("Username", "Password", "Domain", false)]
        public void UserFactAttribute_WithDomain_BuildsUserContextSettings(string username, string password, string domain, bool displayName)
        {
            // Arrange
            var attribute = new UserFactAttribute(username: username, password: password, domain: domain, displayNameOnTest: displayName);

            // Act
            var result = attribute.UserContext.GetCredentials().ToString();

            // Assert
            Assert.Equal($"{username}@{domain}", result);
            Assert.Equal(displayName, attribute.UserContext.DisplayNameOnTest);
        }

        [Theory]
        [InlineData("Username", "Password", true)]
        [InlineData("Username", "Password", false)]
        public void UserFactAttribute_WithUsername_BuildsUserContextSettings(string username, string password, bool displayName)
        {
            // Arrange
            var attribute = new UserTheoryAttribute(username: username, password: password, displayNameOnTest: displayName);

            // Act
            var result = attribute.UserContext.GetCredentials().ToString();

            // Assert
            Assert.Equal($"{username}", result);
            Assert.Equal(displayName, attribute.UserContext.DisplayNameOnTest);
        }      
    }
}
