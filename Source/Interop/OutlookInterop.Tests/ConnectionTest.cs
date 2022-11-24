using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeCharm.OutlookInterfaces;

using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.Outlook;

using Xunit.Abstractions;

namespace CodeCharm.OutlookInterop.Tests
{
    public class ConnectionTest
        : BaseTest
    {
        private readonly IOutlookSession _sut;

        public ConnectionTest(ITestOutputHelper output)
            : base(output)
        {
            // arrange
            var builder = Connection.CreateBuilder();
            _sut = builder
                .WithFeedback(Feedback)
                .Build();
        }


        [Fact]
        public void GetPrimaryExchangeStore()
        {
            // arrange

            // act
            var store = _sut.PrimaryExchangeStore;

            // assert
            store.Should().NotBeNull();
            Feedback.LogDebug(store.DisplayName);
        }


        [Fact]
        public void GetAdditionalExchangeStores()
        {
            // arrange

            // act
            var stores = _sut.AdditionalExchangeStores;

            // assert
            stores.Should().NotBeEmpty();
            foreach (var store in stores)
            {
                Feedback.LogDebug($"Store: {store.DisplayName}");
            }
        }
    }
}
