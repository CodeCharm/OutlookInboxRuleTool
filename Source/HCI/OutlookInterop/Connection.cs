using CodeCharm.Diagnostic;

using Microsoft.Extensions.Logging;

using Exception = System.Exception;
using OutlookApplication = Microsoft.Office.Interop.Outlook.Application;
using OutlookNameSpace = Microsoft.Office.Interop.Outlook.NameSpace;

namespace CodeCharm.OutlookInterop;

public class Connection
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

    public static ConnectionBuilder CreateBuilder() => new();

    public OutlookApplication Application => _application;

    public OutlookNameSpace Session => _session;

    public bool Connect()
    {
        using var _ = _feedback.BeginScope("Connecting");

        if (_application is not null)
        {
            _feedback.LogWarning("Outlook Application is already connected");
            return true;
        }

        try
        {
            _application = new OutlookApplication();
            _session = _application.Session;
        }
        catch (Exception ex)
        {
            _feedback.LogError(ex, "Failed to connect to Outlook");
            return false;
        }

        return true;
    }

}