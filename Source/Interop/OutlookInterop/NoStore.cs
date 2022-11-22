using System.Runtime.CompilerServices;

using CodeCharm.OutlookInterfaces;

using Microsoft.Office.Interop.Outlook;

namespace CodeCharm.OutlookInterop
{
    public class NoStore
        : IStore
    {

		#region Singleton
		private readonly static object c_syncLockObject = new object();
		private volatile static NoStore c_singleton;

		private NoStore()
		{
		}

		public static NoStore Instance
		{
			get
			{
				if (null == c_singleton)
				{
					lock (c_syncLockObject)
					{
						if (null == c_singleton)
						{
							c_singleton = new NoStore();
						}
					}
				}
				return c_singleton;
			}
		}

		#endregion


		public IFolder RootMessageFolder => NoFolder.Instance;

        public string DisplayName => "Store not found; no name to show.";

        public OlExchangeStoreType ExchangeStoreType => throw new System.NotImplementedException("Store not found; no ExchangeStoreType");
    }
}