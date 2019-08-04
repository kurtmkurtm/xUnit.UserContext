using SimpleImpersonation;
using xUnit.UserContext.Configuration;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext
{    
     /// <summary>
     /// Attribute that adds user login information to be used in tests based on the xunit theory attribute
     /// </summary>
    [XunitTestCaseDiscoverer("Xunit.UserContext.XunitExtensions.UserTheoryDiscoverer", "xUnit.UserContext")]
    public sealed class UserTheoryAttribute : TheoryAttribute, IUserContextTest
    {
        /// <summary>
        /// Adds user settings for test impersonation
        /// </summary>
        /// <param name="userSecretsId">Id used to retrieve user credentials for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserTheoryAttribute(string userSecretsId, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(userSecretsId, logonType, displayNameOnTest);

        /// <summary>
        /// Adds user settings for test impersonation
        /// </summary>
        /// <param name="username">Username to use for test impersonation</param>
        /// <param name="password">Password to use for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserTheoryAttribute(string username, string password, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(username, password, logonType, displayNameOnTest);

        /// <summary>
        /// Adds user settings for test impersonation
        /// </summary>
        /// <param name="username">Username to use for test impersonation</param>
        /// <param name="password">Password to use for test impersonation</param>
        /// <param name="domain">Domain that user belongs to</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserTheoryAttribute(string username, string password, string domain, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(username, password, domain, logonType, displayNameOnTest);

        /// <summary>
        /// User settings for test
        /// </summary>
        public UserContextSettings UserContext { get; }
    }
}
