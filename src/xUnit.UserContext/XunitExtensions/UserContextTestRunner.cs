using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    /// <summary>
    /// Test runner that passes through user settings
    /// </summary>
    public class UserContextTestRunner : XunitTestRunner
    {
        private readonly UserContextSettings _userContext;

        /// <summary>
        /// Initialises a new instance of the <see cref="UserContextTestRunner"/> class.
        /// </summary>
        /// <param name="test">The test that this invocation belongs to.</param>
        /// <param name="messageBus">The message bus to report run status to.</param>
        /// <param name="testClass">The test class that the test method belongs to.</param>
        /// <param name="constructorArguments">The arguments to be passed to the test class constructor.</param>
        /// <param name="testMethod">The test method that will be invoked.</param>
        /// <param name="testMethodArguments">The arguments to be passed to the test method.</param>
        /// <param name="skipReason">The skip reason, if the test is to be skipped.</param>
        /// <param name="beforeAfterAttributes">The list of <see cref="BeforeAfterTestAttribute"/>s for this test.</param>
        /// <param name="exceptionAggregator">The exception aggregator used to run code and collect exceptions.</param>
        /// <param name="cancellationTokenSource">The task cancellation token source, used to cancel the test run.</param>
        /// <param name="userContext">The user context settings for impersonation</param>
        public UserContextTestRunner(XunitTest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes, ExceptionAggregator exceptionAggregator, CancellationTokenSource cancellationTokenSource, UserContextSettings userContext)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, beforeAfterAttributes, exceptionAggregator, cancellationTokenSource)
        {
            _userContext = userContext;
        }

        /// <summary>
        /// Override to use UserContextTestInvoker to invoke tests
        /// </summary>
        /// <param name="aggregator"></param>
        /// <returns></returns>
        protected override Task<decimal> InvokeTestMethodAsync(ExceptionAggregator aggregator)
        {
            return new UserContextTestInvoker(Test, MessageBus, TestClass, ConstructorArguments, TestMethod, TestMethodArguments, BeforeAfterAttributes, aggregator, CancellationTokenSource, _userContext).RunAsync();
        }
    }
}

