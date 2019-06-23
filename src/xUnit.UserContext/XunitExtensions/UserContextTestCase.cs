using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserContextTestCase : XunitTestCase
    {
        private readonly UserContextSettings _userContext;

        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public UserContextTestCase() : base() { }

        public UserContextTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions, ITestMethod testMethod, UserContextSettings userContext, object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments)
        {
            _userContext = userContext;
        }

        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus, object[] constructorArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new UserContextTestCaseRunner(this, DisplayName, SkipReason, constructorArguments, TestMethodArguments, messageBus, aggregator, cancellationTokenSource, _userContext).RunAsync();
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (_userContext.DisplayNameOnTest)            
                DisplayName += $"_(user: {_userContext?.DisplayName})";            
        }
    }
}
