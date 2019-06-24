using SimpleImpersonation;
using System;
using xUnit.UserContext.Configuration;
using Xunit;
using Xunit.UserContext.Configuration;

namespace xUnit.UserContext.Tests.Configuration
{
    public class UserContextSettingsTests
    {
        [Fact]
        public void GetCredentials_WithNullCredentials_ThrowsOnGetCredentials()
        {
            // Arrange
            var unitUnderTest = new UserContextSettings(null, null, null, Default.Logon, false);

            // Act
            Action act = () => unitUnderTest.GetCredentials();

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void GetCredentials_WithValidStrings_ReturnsUserCredentials()
        {
            // Arrange
            var unitUnderTest = new UserContextSettings("hello", "world", Default.Logon, false);

            // Act
            var credentials = unitUnderTest.GetCredentials();

            // Assert
            Assert.IsAssignableFrom<UserCredentials>(credentials);
        }
    }
}
