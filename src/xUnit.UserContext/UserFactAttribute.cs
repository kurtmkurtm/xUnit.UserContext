using SimpleImpersonation;
using xUnit.UserContext.Configuration;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext
{
    /// <summary>
    /// Attribute that adds user login information to be used in tests based on the xunit fact attribute
    /// </summary>
    [XunitTestCaseDiscoverer("Xunit.UserContext.XunitExtensions.UserFactDiscoverer", "xUnit.UserContext")]
    public sealed class UserFactAttribute : FactAttribute, IUserContextTest
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UserFactAttribute"/> class.
        /// </summary>
        /// <param name="userSecretsId">Id used to retrieve user credentials for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserFactAttribute(string userSecretsId, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(userSecretsId, logonType, displayNameOnTest);

        /// <summary>
        /// Initialises a new instance of the <see cref="UserFactAttribute"/> class.
        /// </summary>
        /// <param name="username">Username to use for test impersonation</param>
        /// <param name="password">Password to use for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserFactAttribute(string username, string password, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(username, password, logonType, displayNameOnTest);

        /// <summary>
        /// Initialises a new instance of the <see cref="UserFactAttribute"/> class.
        /// </summary>
        /// <param name="username">Username to use for test impersonation</param>
        /// <param name="password">Password to use for test impersonation</param>
        /// <param name="domain">Domain that user belongs to</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserFactAttribute(string username, string password, string domain, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(username, password, domain, logonType, displayNameOnTest);

        /// <summary>
        /// User settings for test
        /// </summary>
        public UserContextSettings UserContext { get; }
    }
}
