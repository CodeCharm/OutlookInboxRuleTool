using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Xunit;
using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests
{
    public class StoreTest
        : BaseTest
    {
        public StoreTest(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void ListOfStoreNames()
        {
            // arrange
            var builder = Connection.CreateBuilder();
            var sut = builder
                .WithFeedback(Feedback)
                .Build();

            // act
            var stores = sut.Stores;

            // assert
            stores.Should().NotBeEmpty("Must have at least one store");
            foreach (var store in stores)
            {
                Feedback.LogDebug(store.DisplayName);
            }
        }
    }
}
