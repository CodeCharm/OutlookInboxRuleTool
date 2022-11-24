using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Azure.Core;
using Azure.Identity;

using CodeCharm.Diagnostic;
using CodeCharm.OutlookInterfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Beta;
using Microsoft.Graph.Beta.Models;

namespace CodeCharm.GraphInterop
{
    public partial class Connection
        : IGraphSession
    {
        private readonly IFeedback _feedback;
        private readonly InteractiveBrowserCredential _interactiveCredential;
        private readonly IEnumerable<string> _scopes;
        private readonly TokenCredential _tokenCredential;
        private GraphServiceClient _client;

        private Connection(IServiceCollection services, IEnumerable<string> scopes)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var provider = services.BuildServiceProvider();
            var feedback = provider.GetService<IFeedback>();
            _feedback = feedback;

            _scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));
        }

        internal Connection(IServiceCollection services, InteractiveBrowserCredential interactiveCredential, IEnumerable<string> scopes)
            : this(services, scopes)
        {
            _interactiveCredential = interactiveCredential ?? throw new ArgumentNullException(nameof(interactiveCredential));
        }

        internal Connection(IServiceCollection services, TokenCredential tokenCredential, IEnumerable<string> scopes)
            : this(services, scopes)
        {
            _tokenCredential = tokenCredential ?? throw new ArgumentNullException(nameof(tokenCredential));
        }

        public static ConnectionBuilder CreateBuilder() => new ConnectionBuilder();

        public bool Connected => null != _client;

        internal bool AutoConnect()
        {
            if (null == _client)
            {
                _feedback.LogDebug("Automatically connecting");

                var connected = Connect();
                if (!connected)
                {
                    throw new InvalidOperationException("Could not connect to Graph session");
                }
                return connected;
            }
            else
                return true;
        }

        public bool Connect()
        {
            using (var _ = _feedback.BeginScope(this))
            {
                if (null != _client)
                {
                    _feedback.LogInformation("Graph Service Client is already connected");
                    return true;
                }

                try
                {
                    if (null != _interactiveCredential)
                    {
                        _feedback.LogTrace("Connecting with InteractiveCredential");
                        _client = new GraphServiceClient(_interactiveCredential, _scopes);
                    }
                    else if (null != _tokenCredential)
                    {
                        _feedback.LogTrace("Connecting with TokenCredential");
                        _client = new GraphServiceClient(_tokenCredential, _scopes);
                    }
                    else
                    {
                        throw new InvalidOperationException("No credential provider available");
                    }
                }
                catch (Exception ex)
                {
                    _feedback.LogError(ex, "Failed to connect to Microsoft Graph");
                    return false;
                }

                return true;
            }
        }

        public async Task<User> GetMeAsync()
        {
            using (var _ = _feedback.BeginScope(this))
            {
                if (AutoConnect())
                {
                    return await _client
                        .Me
                        .GetAsync();
                }
                else
                {
                    return await Task.FromResult((User)null);
                }
            }
        }
    }
}
