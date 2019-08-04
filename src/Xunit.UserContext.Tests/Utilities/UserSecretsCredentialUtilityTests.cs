using System.Net;
using Xunit;
using Xunit.UserContext.Services;

namespace xUnit.UserContext.Tests.Utilities
{
    public class UserSecretsCredentialUtilityTests
    {
        [Fact]
        public void GetCredentials_WithExistingSecretsId_ReturnsValidCredentials()
        {
            // Arrange
            var userSecretsId = "IntergrationTestID123";
            var userProvider = new UserSecretsProvider(userSecretsId);

            // Act
            var result = userProvider.GetCredentials();

            // Assert
            Assert.IsAssignableFrom<NetworkCredential>(result);
        }

        [Fact]
        public void GetCredentials_WithUnsetSecretsId_ReturnsEmptyAndDoesntThrow()
        {
            // Arrange
            var userSecretsId = "NonExistantId";
            var userProvider = new UserSecretsProvider(userSecretsId);

            // Act
            var result = userProvider.GetCredentials();

            // Assert
            Assert.True(string.IsNullOrEmpty(result.UserName));
        }
    }
}
