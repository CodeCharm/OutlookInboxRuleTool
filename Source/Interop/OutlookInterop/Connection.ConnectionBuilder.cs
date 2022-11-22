﻿using CodeCharm.Diagnostic;

using Microsoft.Extensions.DependencyInjection;

namespace CodeCharm.OutlookInterop
{
    public partial class Connection
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
}