using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserContextTestRunner : XunitTestRunner
    {
        private UserContextSettings _userContext;

        public UserContextTestRunner(XunitTest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes, ExceptionAggregator exceptionAggregator, CancellationTokenSource cancellationTokenSource, UserContextSettings userContext)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, beforeAfterAttributes, exceptionAggregator, cancellationTokenSource)
        {
            _userContext = userContext;
        }

        protected override Task<decimal> InvokeTestMethodAsync(ExceptionAggregator aggregator)
        {
            return new UserContextTestInvoker(Test, MessageBus, TestClass, ConstructorArguments, TestMethod, TestMethodArguments, BeforeAfterAttributes, aggregator, CancellationTokenSource, _userContext).RunAsync();
        }
    }
}

