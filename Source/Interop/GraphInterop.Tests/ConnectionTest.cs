namespace CodeCharm.GraphInterop.Tests;

public class ConnectionTest
    : BaseTest
{
    private readonly IGraphSession _sut;
    public ConnectionTest(ITestOutputHelper output)
        : base(output)
    {
        // arrange
        var builder = Connection.CreateBuilder();
        _sut = builder
            .WithFeedback(Feedback)
            .AddDefaultScopes()
            //.UseInteractiveAuthenticationProvider()
            .UseBearerAccessTokenProvider()
            .Build();
    }

    [Fact]
    public async Task GetUserMe()
    {
        // arrange
        // act
        var me = await _sut.GetMeAsync();
        // assert
        me.Should().NotBeNull();
        //var serializedMe = JsonSerializer.Serialize<User>(me, new JsonSerializerOptions()
        //{
        //    WriteIndented = true
        //});
        //Feedback.LogInformation(serializedMe);
    }
}
