using SimpleImpersonation;
using System.Net;
using Xunit.UserContext.Services;

namespace Xunit.UserContext.Configuration
{
    public class UserContextSettings
    {
        private readonly NetworkCredential _credenitals; // Doesn't throw on nulls - avoids exceptions in constructor during discovery

        public UserContextSettings(string username, string password, LogonType logonType, bool displayNameOnTest)
            : this(new NetworkCredential(username, password), logonType, displayNameOnTest) { }

        public UserContextSettings(string username, string password, string domain, LogonType logonType, bool displayNameOnTest)
            : this(new NetworkCredential(username, password, domain), logonType, displayNameOnTest) { }

        public UserContextSettings(string userSecretsId, LogonType logonType, bool displayNameOnTest) :
            this(new UserSecretsProvider(userSecretsId).GetCredentials(), logonType, displayNameOnTest) { }

        public UserContextSettings(NetworkCredential networkCredential, LogonType logonType, bool displayNameOnTest)
        {
            _credenitals = networkCredential;
            LogonType = logonType;
            DisplayNameOnTest = displayNameOnTest;
        }

        public LogonType LogonType { get; }
        public bool DisplayNameOnTest { get; }
        public string DisplayName => _credenitals.UserName;

        public UserCredentials GetCredentials()
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
