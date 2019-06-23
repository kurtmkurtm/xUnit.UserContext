using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserContextTestCaseRunner : XunitTestCaseRunner
    {
        private readonly UserContextSettings _userContext;

        public UserContextTestCaseRunner(IXunitTestCase testCase, string displayName, string skipReason, object[] constructorArguments, object[] testMethodArguments, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, UserContextSettings userContext)
            : base(testCase, displayName, skipReason, constructorArguments, testMethodArguments, messageBus, aggregator, cancellationTokenSource) { _userContext = userContext; }

        protected override Task<RunSummary> RunTestAsync()
        {
            var test = new XunitTest(TestCase, DisplayName);
            return new UserContextTestRunner(test, MessageBus, TestClass, ConstructorArguments, TestMethod, TestMethodArguments, SkipReason, BeforeAfterAttributes, new ExceptionAggregator(Aggregator), CancellationTokenSource, _userContext).RunAsync();
        }
    }
}
