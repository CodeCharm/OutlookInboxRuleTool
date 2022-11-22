namespace CodeCharm.OutlookInterfaces
{
    public interface IConnection
    {
        bool Connected { get; }
        IFolder DefaultStoreRootFolder { get; }
        IStores Stores { get; }
        IStore PrimaryExchangeStore { get; }
        IStores AdditionalExchangeStores { get; }

        bool Connect();
    }
}