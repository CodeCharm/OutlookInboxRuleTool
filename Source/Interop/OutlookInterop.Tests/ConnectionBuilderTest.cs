using CodeCharm.Diagnostic;
using CodeCharm.OutlookInterop;
using CodeCharm.Test.Diagnostic;

using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests;

public class ConnectionBuilderTest
	: BaseTest
{
	public ConnectionBuilderTest(ITestOutputHelper output)
		: base(output)
	{
	}

	[Fact]
	public void ConnectProducesReferenceToOutlookApplication()
	{
		// arrange
		var builder = Connection.CreateBuilder();
		
		// act
		var sut = builder
			.WithFeedback(Feedback)
			.Build();
		sut.Connect();
		var actual = sut.Connected;

		// assert
		actual.Should().BeTrue("Connection should have been made");
	}

}