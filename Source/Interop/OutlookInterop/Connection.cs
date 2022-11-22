using System;

using CodeCharm.Diagnostic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Exception = System.Exception;
using OutlookApplication = Microsoft.Office.Interop.Outlook.Application;
using OutlookNameSpace = Microsoft.Office.Interop.Outlook.NameSpace;

namespace CodeCharm.OutlookInterop
{
    public partial class Connection
        : IConnection
    {
        private readonly IFeedback _feedback;
        private OutlookApplication _application;
        private OutlookNameSpace _session;

        internal Connection(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var feedback = provider.GetService<IFeedback>();
            _feedback = feedback;
        }

        public static ConnectionBuilder CreateBuilder() => new ConnectionBuilder();

        internal OutlookApplication Application => _application;

        internal OutlookNameSpace Session => _session;

        public bool Connect()
        {
            using (var _ = _feedback.BeginScope(this))
            {
                if (null != _application)
                {
                    _feedback.LogWarning("Outlook Application is already connected");
                    return true;
                }

                try
                {
                    _application = new OutlookApplication();
                    _session = _application.Session;
                    _feedback.LogInformation($"Session: {_session.CurrentProfileName}");
                }
                catch (Exception ex)
                {
                    _feedback.LogError(ex, "Failed to connect to Outlook");
                    return false;
                }

                return true;
            }
        }

        public bool Connected => null != _session;

        internal bool AutoConnect()
        {
            if (null == _session)
            {
                _feedback.LogDebug("Automatically connecting");

                var connected = Connect();
                if (!connected)
                {
                    throw new InvalidOperationException("Could not connect to Outlook session");
                }
                return connected;
            }
            else
                return true;
        }

        public IFolder DefaultStoreRootFolder
        {
            get
            {
                using (var _ = _feedback.BeginScope(this))
                {
                    if (AutoConnect())
                    {
                        var olkFolder = _session.DefaultStore.GetRootFolder();
                        var ccFolder = new Folder(olkFolder);
                        return ccFolder;
                    }
                    else
                    {
                        return NoFolder.Instance;
                    }
                }
            }
        }

        public IStores Stores
        {
            get
            {
                using (var _ = _feedback.BeginScope(this))
                {
                    if (AutoConnect())
                    {
                        var stores = new Stores(_session.Stores);
                        return stores;
                    }
                    else
                    {
                        return NoStores.Instance;
                    }
                }
            }
        }

    }
}