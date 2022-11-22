using System.Collections;
using System.Collections.Generic;

using OutlookStore = Microsoft.Office.Interop.Outlook.Store;
using OutlookStores = Microsoft.Office.Interop.Outlook.Stores;

namespace CodeCharm.OutlookInterop
{
    public class Stores
        : IStores
    {
        private readonly OutlookStores _stores;

        internal Stores(OutlookStores outlookStores)
        {
            _stores = outlookStores ?? throw new System.ArgumentNullException(nameof(outlookStores));
        }

        public IEnumerator<IStore> GetEnumerator()
        {
            foreach (OutlookStore outlookStore in _stores)
            {
                yield return new Store(outlookStore);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}