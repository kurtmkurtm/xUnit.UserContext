using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserContextTestCase : XunitTestCase
    {
        private UserContextSettings _userContext;

        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public UserContextTestCase() : base() { }

        public UserContextTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions, ITestMethod testMethod, object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments)
        { }

        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus, object[] constructorArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new UserContextTestCaseRunner(this, DisplayName, SkipReason, constructorArguments, TestMethodArguments, messageBus, aggregator, cancellationTokenSource, _userContext).RunAsync();
        }

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
