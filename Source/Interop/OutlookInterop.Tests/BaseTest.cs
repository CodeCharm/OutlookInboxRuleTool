using CodeCharm.Diagnostic;
using CodeCharm.Test.Diagnostic;

using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests
{
    public class BaseTest
    {
        protected readonly IFeedback Feedback;
     
        public BaseTest(ITestOutputHelper output)
        {
            Feedback = new Feedback(output);
        }
    }
}