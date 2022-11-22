using System.Collections;
using System.Collections.Generic;

using CodeCharm.OutlookInterfaces;

using OutlookStore = Microsoft.Office.Interop.Outlook.Store;
using OutlookStores = Microsoft.Office.Interop.Outlook.Stores;

namespace CodeCharm.OutlookInterop
{
    public class Stores
        : IStores
    {
        private readonly OutlookStores _stores;
        private readonly IEnumerable<IStore> _storesSubset;

        internal Stores(OutlookStores outlookStores)
        {
            _stores = outlookStores ?? throw new System.ArgumentNullException(nameof(outlookStores));
        }

        internal Stores(IEnumerable<IStore> stores) => _storesSubset = stores;

        public IEnumerator<IStore> GetEnumerator()
        {
            if (null == _storesSubset)
            {
                foreach (OutlookStore outlookStore in _stores)
                {
                    yield return new Store(outlookStore);
                }
            }
            else
            {
                foreach (var store in _storesSubset)
                {
                    yield return store;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}