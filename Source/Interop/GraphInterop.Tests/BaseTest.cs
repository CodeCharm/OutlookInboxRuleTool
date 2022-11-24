namespace CodeCharm.GraphInterop.Tests;

public class BaseTest
{
    protected readonly IFeedback Feedback;
    public BaseTest(ITestOutputHelper output)
    {
        Feedback = new Feedback(output);
    }
}