using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeCharm.Diagnostic;
using CodeCharm.Test.Diagnostic;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests;

public class FolderTest
	: BaseTest
{
	public FolderTest(ITestOutputHelper output)
		: base(output)
	{
	}


	[Fact]
	public void Test()
	{
        // arrange
        var builder = Connection.CreateBuilder();
		var sut = builder
			.WithFeedback(Feedback)
			.Build();
		sut.Connect();

		// act
		var folder = sut.DefaultStoreRootFolder;

		// assert
		Feedback.LogInformation($"Folder path: {folder.Path}");
    }
}
