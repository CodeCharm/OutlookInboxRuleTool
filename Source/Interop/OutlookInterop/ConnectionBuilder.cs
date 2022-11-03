using System;

using CodeCharm.Diagnostic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;


namespace CodeCharm.OutlookInterop
{
    public class ConnectionBuilder
    {
        private readonly IServiceCollection _services;

        internal ConnectionBuilder()
        {
            _services = new ServiceCollection();
        }

        public IServiceCollection Services => _services;

        public Connection Build()
        {
            return new Connection(Services);
        }

        public ConnectionBuilder WithFeedback(IFeedback feedback)
        {
            _services.AddSingleton(feedback);
            return this;
        }
    }
}