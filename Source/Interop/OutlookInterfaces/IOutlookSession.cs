using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCharm.OutlookInterfaces
{
    public interface IOutlookSession
        : IConnection
    {
        IFolder DefaultStoreRootFolder { get; }
        IStores Stores { get; }
        IStore PrimaryExchangeStore { get; }
        IStores AdditionalExchangeStores { get; }
    }
}
