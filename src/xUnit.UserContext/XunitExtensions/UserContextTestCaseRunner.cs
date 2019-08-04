using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    /// <summary>
    /// Test case runner that passes through user settings
    /// </summary>
    public class UserContextTestCaseRunner : XunitTestCaseRunner
    {
        private readonly UserContextSettings _userContext;

        /// <summary>
        /// Initialises a new instance of <see cref="UserContextTestCaseRunner"/>
        /// </summary>
        /// <param name="testCase"></param>
        /// <param name="displayName"></param>
        /// <param name="skipReason"></param>
        /// <param name="constructorArguments"></param>
        /// <param name="testMethodArguments"></param>
        /// <param name="messageBus"></param>
        /// <param name="aggregator"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <param name="userContext"></param>
        public UserContextTestCaseRunner(IXunitTestCase testCase, string displayName, string skipReason, object[] constructorArguments, object[] testMethodArguments, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, UserContextSettings userContext)
            : base(testCase, displayName, skipReason, constructorArguments, testMethodArguments, messageBus, aggregator, cancellationTokenSource) { _userContext = userContext; }

        /// <summary>
        /// Override test run to use UserContextTestRunner
        /// </summary>
        /// <returns>Test result<see cref="RunSummary"/></returns>
        protected override Task<RunSummary> RunTestAsync()
        {
            var test = new XunitTest(TestCase, DisplayName);
            return new UserContextTestRunner(test, MessageBus, TestClass, ConstructorArguments, TestMethod, TestMethodArguments, SkipReason, BeforeAfterAttributes, new ExceptionAggregator(Aggregator), CancellationTokenSource, _userContext).RunAsync();
        }
    }
}
