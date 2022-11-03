namespace CodeCharm.OutlookInterop;

public class ConnectionBuilder
{
    private ServiceCollection _services;

    internal ConnectionBuilder()
    {
        _services = new ServiceCollection();
    }

    public ServiceCollection Services => _services;

    public Connection Build()
    {
        return new Connection(Services);
    }
}
