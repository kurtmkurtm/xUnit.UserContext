using Xunit.UserContext.Configuration;

namespace Xunit.UserContext
{
    /// <summary>
    /// Interface for custom test attributes to read user settings on test invocation
    /// </summary>
    internal interface IUserContextTest
    {
        /// <summary>
        /// User settings for test
        /// </summary>
        UserContextSettings UserContext { get; }
    }
}