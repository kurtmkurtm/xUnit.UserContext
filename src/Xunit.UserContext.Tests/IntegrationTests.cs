using System;
using System.Reflection;

namespace Xunit.UserContext.Tests
{
    /// <summary>
    /// To create accounts for test run IntegrationTestSetup.cmd
    /// To remove tests accounts run IntegrationTestTearDown.cmd 
    /// Both scripts should be ran in an elevated command prompt
    /// </summary>
    public class IntegrationTests
    {
        public IntegrationTests() => Assembly.Load("xunit.assert");

        [UserTheory("TestUsername", "ICV$8p0ARL!m")]
        [InlineData(false)]
        public void UserTheory_WithValidCredentials_RunsUnderContextOfAccount(bool discard)
        {
            _ = discard;
            var expectedUser = "TestUsername";

            var actualUser = Environment.UserName;

            Assert.Equal(expectedUser, actualUser);
        }

        [UserFact("TestUsername", "ICV$8p0ARL!m")]
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
    }
}
