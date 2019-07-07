using Xunit.UserContext.Configuration;

namespace Xunit.UserContext
{
    internal interface IUserContextTest
    {
        UserContextSettings UserContext { get; }
    }
}