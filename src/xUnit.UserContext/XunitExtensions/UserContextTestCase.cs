using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    /// <summary>
    /// UserContextTestCase implementation of Xunit.Sdk.IXunitTestCase for xUnit v2 that supports
    /// tests decorated with both Xunit.UserFactAttribute and Xunit.UserTheoryAttribute.
    /// </summary>
    public class UserContextTestCase : XunitTestCase
    {
        private UserContextSettings _userContext;

        /// <summary>
        /// Initialises a new instance of <see cref="UserContextTestCase"/>
        /// </summary>
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public UserContextTestCase() : base() { }


        /// <summary>
        /// Initialises a new instance of <see cref="UserContextTestCase"/>
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        /// <param name="defaultMethodDisplay">Default method display</param>
        /// <param name="defaultMethodDisplayOptions">Default method display options </param>
        /// <param name="testMethod">The test method</param>
        /// <param name="testMethodArguments">The arguments for the test method.</param>
        public UserContextTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions, ITestMethod testMethod, object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments)
        { }


        /// <summary>
        /// Override test run to use UserContextTestCaseRunner
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        /// <param name="messageBus">The message bus to report run status to.</param>
        /// <param name="constructorArguments">The arguments to be passed to the test class constructor.</param>
        /// <param name="aggregator">The exception aggregator used to run code and collect exceptions.</param>
        /// <param name="cancellationTokenSource">The task cancellation token source, used to cancel the test run.</param>
        /// <returns></returns>
        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus, object[] constructorArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new UserContextTestCaseRunner(this, DisplayName, SkipReason, constructorArguments, TestMethodArguments, messageBus, aggregator, cancellationTokenSource, _userContext).RunAsync();
        }

        /// <summary>
        /// Override base class to customise test display name to include username 
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            var userContextTestAttributes = TestMethod.Method.GetCustomAttributes(typeof(IUserContextTest)).First();

            _userContext = userContextTestAttributes.GetNamedArgument<UserContextSettings>(nameof(UserTheoryAttribute.UserContext));

            if (_userContext.DisplayNameOnTest)
                DisplayName += $"_(user: {_userContext?.DisplayName})";
        }
    }
}
