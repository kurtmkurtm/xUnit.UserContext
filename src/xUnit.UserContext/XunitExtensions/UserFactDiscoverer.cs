using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.UserContext.XunitExtensions
{
    public class UserFactDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;
        readonly FactDiscoverer factDiscoverer;

        public UserFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
            factDiscoverer = new FactDiscoverer(diagnosticMessageSink);
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var defaultMethodDisplay = discoveryOptions.MethodDisplayOrDefault();
            var defaultMethodDisplayOptions = discoveryOptions.MethodDisplayOptionsOrDefault();

            return factDiscoverer.Discover(discoveryOptions, testMethod, factAttribute)
                     .Select(testCase =>
                     new UserContextTestCase(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod));
        }
    }

}
