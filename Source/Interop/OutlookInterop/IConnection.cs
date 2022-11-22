namespace CodeCharm.OutlookInterop
{
    public interface IConnection
    {
        bool Connected { get; }
        IFolder DefaultStoreRootFolder { get; }
        IStores Stores { get; }

        bool Connect();
    }
}