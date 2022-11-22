using CodeCharm.Diagnostic;
using CodeCharm.OutlookInterop;
using CodeCharm.Test.Diagnostic;

using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests;

public class ConnectionBuilderTest
{
	private readonly IFeedback _feedback;

	public ConnectionBuilderTest(ITestOutputHelper output)
	{
		_feedback = new Feedback(output);
	}

	[Fact]
	public void ConnectProducesReferenceToOutlookApplication()
	{
		// arrange
		var builder = Connection.CreateBuilder();
		
		// act
		var sut = builder
			.WithFeedback(_feedback)
			.Build();
		sut.Connect();
		var expected = sut.Application;

		// assert
		expected.Should().NotBeNull("An application object should have been returned");
	}

}