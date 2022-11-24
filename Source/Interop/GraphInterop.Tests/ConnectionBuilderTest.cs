namespace CodeCharm.GraphInterop.Tests;

public class ConnectionBuilderTest
    : BaseTest
{
    public ConnectionBuilderTest(ITestOutputHelper output)
        : base(output)
    {
    }
    [Fact]
    public void ConnectProducesReferenceToGraph()
    {
        // arrange
        var builder = Connection.CreateBuilder();
        // act
        var sut = builder
            .WithFeedback(Feedback)
            .AddDefaultScopes()
            //.UseInteractiveAuthenticationProvider()
            .UseBearerAccessTokenProvider()
            .Build();
        sut.Connect();
        var actual = sut.Connected;
        // assert
        actual.Should().BeTrue("Connection should have been made");
    }
}