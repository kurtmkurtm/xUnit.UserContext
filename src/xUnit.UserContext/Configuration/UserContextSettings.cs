using SimpleImpersonation;
using System.Net;
using Xunit.UserContext.Services;

namespace Xunit.UserContext.Configuration
{
    /// <summary>
    /// Created by the test attributes to holds credentials and related settings
    /// </summary>
    public class UserContextSettings
    {
        // NetworkCredential allows construction during discovery without throwing on empty credentials
        private readonly NetworkCredential _credenitals;

        /// <summary>
        /// Initialises a new instance of the <see cref="UserContextSettings"/> class.
        /// </summary>
        /// <param name="username">Username to use for test impersonation</param>
        /// <param name="password">Password to use for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserContextSettings(string username, string password, LogonType logonType, bool displayNameOnTest)
            : this(new NetworkCredential(username, password), logonType, displayNameOnTest) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="UserContextSettings"/> class.
        /// </summary>
        /// <param name="username">Username to use for test impersonation</param>
        /// <param name="password">Password to use for test impersonation</param>
        /// <param name="domain">Domain that user belongs to</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append user name to test</param>
        public UserContextSettings(string username, string password, string domain, LogonType logonType, bool displayNameOnTest)
            : this(new NetworkCredential(username, password, domain), logonType, displayNameOnTest) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="UserContextSettings"/> class.
        /// </summary>
        /// <param name="userSecretsId">Id used to retrieve user credentials for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append user name to test</param>
        public UserContextSettings(string userSecretsId, LogonType logonType, bool displayNameOnTest) :
            this(new UserSecretsProvider(userSecretsId).GetCredentials(), logonType, displayNameOnTest)
        { }

        /// <summary>
        /// Initialises a new instance of the <see cref="UserContextSettings"/> class.
        /// </summary>
        /// <param name="networkCredential">Credentials to use for test impersonation</param>
        /// <param name="logonType">Logon method to use</param>
        /// <param name="displayNameOnTest">Append username to test</param>
        public UserContextSettings(NetworkCredential networkCredential, LogonType logonType, bool displayNameOnTest)
        {
            _credenitals = networkCredential;
            LogonType = logonType;
            DisplayNameOnTest = displayNameOnTest;
        }

        /// <summary>
        /// Returns login type to use in test
        /// </summary>
        public LogonType LogonType { get; }

        /// <summary>
        /// If true displays username at the end of the test name
        /// </summary>
        public bool DisplayNameOnTest { get; }

        /// <summary>
        /// Returns username for use with DisplayNameOnTest
        /// </summary>
        public string DisplayName => _credenitals.UserName;

        /// <summary>
        /// Load credentials in required format to use in test
        /// </summary>
        /// <returns>Creates a SimpleImpersonation.UserCredentials class based on a the credentials present</returns>
        internal UserCredentials GetCredentials()
        {
            if (string.IsNullOrEmpty(_credenitals.Domain))
            {
                return new UserCredentials(_credenitals.UserName, _credenitals.SecurePassword);
            }
            else
            {
                return new UserCredentials(_credenitals.Domain, _credenitals.UserName, _credenitals.SecurePassword);
            }
        }
    }
}