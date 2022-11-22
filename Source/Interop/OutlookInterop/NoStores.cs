using System.Collections;
using System.Collections.Generic;

using CodeCharm.OutlookInterfaces;

namespace CodeCharm.OutlookInterop
{
    public class NoStores
        : IStores
    {

		#region Singleton
		private readonly static object c_syncLockObject = new object();
		private volatile static NoStores c_singleton;

		private NoStores()
		{
		}

		public static NoStores Instance
		{
			get
			{
				if (null == c_singleton)
				{
					lock (c_syncLockObject)
					{
						if (null == c_singleton)
						{
							c_singleton = new NoStores();
						}
					}
				}
				return c_singleton;
			}
		}

        #endregion

        public IEnumerator<IStore> GetEnumerator()
        {
			yield return NoStore.Instance;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

    }
}