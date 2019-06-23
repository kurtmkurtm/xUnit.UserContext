using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserContextTestInvoker : XunitTestInvoker
    {
        private readonly UserContextSettings _userContext;

        public UserContextTestInvoker(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, UserContextSettings userContext)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, beforeAfterAttributes, aggregator, cancellationTokenSource)
        {
            _userContext = userContext;
        }

        //Run test under the context of the provided user
        protected override object CallTestMethod(object testClassInstance)
        {
            return Impersonation.RunAsUser(_userContext.GetCredentials(), _userContext.LogonType,
                () => base.CallTestMethod(testClassInstance));
        }
    }
}
