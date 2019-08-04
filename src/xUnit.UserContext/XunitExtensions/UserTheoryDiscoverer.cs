using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext;

namespace Xunit.UserContext.XunitExtensions
{
    /// <summary>
    /// Implementation of <see cref="IXunitTestCaseDiscoverer"/> that supports finding test cases
    /// on methods decorated with <see cref="UserTheoryAttribute"/>.
    /// </summary>
    public class UserTheoryDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;
        readonly TheoryDiscoverer theoryDiscoverer;

        /// <summary>
        /// Initialises a new instance of the <see cref="UserTheoryDiscoverer"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        public UserTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
            theoryDiscoverer = new TheoryDiscoverer(diagnosticMessageSink);
        }

        /// <summary>
        /// Discover user context test cases from a test method.
        /// </summary>
        /// <param name="discoveryOptions">The discovery options to be used.</param>
        /// <param name="testMethod">The test method the test cases belong to.</param>
        /// <param name="factAttribute">The fact attribute attached to the test method.</param>
        /// <returns>Returns zero or more UserContextTest cases represented by the test method.</returns>
        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var defaultMethodDisplay = discoveryOptions.MethodDisplayOrDefault();
            var defaultMethodDisplayOptions = discoveryOptions.MethodDisplayOptionsOrDefault();

            return theoryDiscoverer.Discover(discoveryOptions, testMethod, factAttribute)
                                   .Select(testCase => new UserContextTestCase(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testCase?.TestMethod, testCase?.TestMethodArguments));
        }

    }
}
