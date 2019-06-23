using System;
using Xunit;
using Xunit.UserContext;

namespace Xunit.UserContext.Tests
{
    /// <summary>
    /// To create accounts for test run IntegrationTestSetup.cmd
    /// To remove tests accounts run IntegrationTestTearDown.cmd 
    /// Both scrips should be ran in an elevated command prompt
    /// </summary>
    public class IntegrationTests
    {
        [UserTheory("TestUsername", "TestPassword")]
        [InlineData(false)]
        public void UserTheory_WithValidCredentials_RunsUnderContextOfAccount(bool discard)
        {
            _ = discard;
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserFact("TestUsername", "TestPassword")]
        public void UserFact_WithValidCredentials_RunsUnderContextOfAccount()
        {
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserFact("IntergrationTestID123")]
        public void UserFact_WithValidSecretsId_RunsUnderContextOfAccount()
        {
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserTheory("IntergrationTestID123")]
        [InlineData(false)]
        public void UserTheory_WithValidSecretsId_RunsUnderContextOfAccount(bool discard)
        {
            _ = discard;
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserTheory("FailUsername", "FailPassword", Skip = "Enable to confirm exception thrown during test, not discovery")]
        [InlineData(false)]
        public void UserTheory_WithInvalidCredentials_RunsUnderContextOfAccount(bool discard)
        {
            _ = discard;
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserFact("FailUsername", "FailPassword", Skip = "Enable to confirm exception thrown during test, not discovery")]
        public void UserFact_WithInvalidCredentials_RunsUnderContextOfAccount()
        {
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserFact("FailTestID123", Skip = "Enable to confirm exception thrown during test, not discovery")]
        public void UserFact_WithInvalidSecretsId_RunsUnderContextOfAccount()
        {
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserTheory("FailTestID123", Skip = "Enable to confirm exception thrown during test, not discovery")]
        [InlineData(false)]
        public void UserTheory_WithInvalidSecretsId_RunsUnderContextOfAccount(bool discard)
        {
            _ = discard;
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

    }
}
