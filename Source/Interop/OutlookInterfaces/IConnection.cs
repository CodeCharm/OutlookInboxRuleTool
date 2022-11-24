namespace CodeCharm.OutlookInterfaces
{
    public interface IConnection
    {
        bool Connected { get; }

        bool Connect();
    }
}