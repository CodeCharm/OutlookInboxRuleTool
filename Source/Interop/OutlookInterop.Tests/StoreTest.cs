using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeCharm.OutlookInterfaces;

using Microsoft.Extensions.Logging;

using Xunit;
using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests
{
    public class StoreTest
        : BaseTest
    {
        private readonly IOutlookSession _sut;

        public StoreTest(ITestOutputHelper output)
            : base(output)
        {
            // arrange
            var builder = Connection.CreateBuilder();
            _sut = builder
                .WithFeedback(Feedback)
                .Build();
        }

        [Fact]
        public void ListOfStoreNames()
        {
            // arrange

            // act
            var stores = _sut.Stores;

            // assert
            stores.Should().NotBeEmpty("Must have at least one store");
            foreach (var store in stores)
            {
                Feedback.LogDebug(store.DisplayName);
            }
        }


        [Fact]
        public void ListOfStoreExchangeTypes()
        {
            // arrange

            // act
            var stores = _sut.Stores;

            // assert
            foreach (var store in stores)
            {
                Feedback.LogDebug($"Store {store.DisplayName}: ExchangeType = {store.ExchangeStoreType}");
            }

        }

    }
}
