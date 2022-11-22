using Microsoft.Office.Interop.Outlook;

namespace CodeCharm.OutlookInterfaces
{
    public interface IStore
    {
        IFolder RootMessageFolder { get; }
        string DisplayName { get; }
        OlExchangeStoreType ExchangeStoreType { get; }
    }
}