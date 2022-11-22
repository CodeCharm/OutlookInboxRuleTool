using System;

using OutlookStore = Microsoft.Office.Interop.Outlook.Store;

namespace CodeCharm.OutlookInterop
{
    public class Store
        : IStore
    {
        private readonly OutlookStore _store;

        internal Store(OutlookStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public IFolder RootMessageFolder
        {
            get {
                var rootFolder = _store.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
                return new Folder(rootFolder); }
        }

        //public Microsoft.Office.Interop.Outlook.Folders GetSearchFolders()
        //{
        //    throw new NotImplementedException();
        //}

        //public Microsoft.Office.Interop.Outlook.Rules GetRules()
        //{
        //    throw new NotImplementedException();
        //}

        //public Microsoft.Office.Interop.Outlook.MAPIFolder GetSpecialFolder(Microsoft.Office.Interop.Outlook.OlSpecialFolders FolderType)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RefreshQuotaDisplay()
        //{
        //    throw new NotImplementedException();
        //}

        //public Microsoft.Office.Interop.Outlook.MAPIFolder GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders FolderType)
        //{
        //    throw new NotImplementedException();
        //}

        //public Microsoft.Office.Interop.Outlook.Application Application => throw new NotImplementedException();

        //public Microsoft.Office.Interop.Outlook.OlObjectClass Class => throw new NotImplementedException();

        //public Microsoft.Office.Interop.Outlook.NameSpace Session => throw new NotImplementedException();

        //public dynamic Parent => throw new NotImplementedException();

        public string DisplayName => _store.DisplayName;

        //public string StoreID => throw new NotImplementedException();

        //public Microsoft.Office.Interop.Outlook.OlExchangeStoreType ExchangeStoreType => throw new NotImplementedException();

        //public string FilePath => throw new NotImplementedException();

        //public bool IsCachedExchange => throw new NotImplementedException();

        //public bool IsDataFileStore => throw new NotImplementedException();

        //public bool IsOpen => throw new NotImplementedException();

        //public dynamic MAPIOBJECT => throw new NotImplementedException();

        //public Microsoft.Office.Interop.Outlook.PropertyAccessor PropertyAccessor => throw new NotImplementedException();

        //public bool IsInstantSearchEnabled => throw new NotImplementedException();

        //public bool IsConversationEnabled => throw new NotImplementedException();

        //public Microsoft.Office.Interop.Outlook.Categories Categories => throw new NotImplementedException();
    }
}
