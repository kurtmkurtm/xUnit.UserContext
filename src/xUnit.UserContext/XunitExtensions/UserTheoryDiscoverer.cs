using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserTheoryDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;
        readonly TheoryDiscoverer theoryDiscoverer;

        public UserTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
            theoryDiscoverer = new TheoryDiscoverer(diagnosticMessageSink);
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var defaultMethodDisplay = discoveryOptions.MethodDisplayOrDefault();
            var defaultMethodDisplayOptions = discoveryOptions.MethodDisplayOptionsOrDefault();

            return theoryDiscoverer.Discover(discoveryOptions, testMethod, factAttribute)
                                   .Select(testCase => new UserContextTestCase(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testCase.TestMethod, testCase.TestMethodArguments));
        }

    }
}
