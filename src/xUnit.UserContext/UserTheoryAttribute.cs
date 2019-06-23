using SimpleImpersonation;
using xUnit.UserContext.Configuration;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext
{
    [XunitTestCaseDiscoverer("Xunit.UserContext.XunitExtensions.UserTheoryDiscoverer", "xUnit.UserContext")]
    public sealed class UserTheoryAttribute : TheoryAttribute
    {
        public UserTheoryAttribute(string userSecretsId, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(userSecretsId, logonType, displayNameOnTest);

        public UserTheoryAttribute(string username, string password, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(username, password, logonType, displayNameOnTest);

        public UserTheoryAttribute(string username, string password, string domain, LogonType logonType = Default.Logon, bool displayNameOnTest = Default.DisplayName)
            => UserContext = new UserContextSettings(username, password, domain, logonType, displayNameOnTest);

        public UserContextSettings UserContext { get; }
    }
}
