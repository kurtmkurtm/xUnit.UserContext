using SimpleImpersonation;

namespace xUnit.UserContext.Configuration
{
    /// <summary>
    /// Default settings for test attributes
    /// </summary>
    public class Default
    {
        /// Use network login
        public const LogonType Logon = LogonType.Network;

        /// Display username at the end of the test
        public const bool DisplayName = true;
    }
}
