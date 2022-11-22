using CodeCharm.OutlookInterfaces;

namespace CodeCharm.OutlookInterop
{
    public class NoFolder
        : IFolder
    {

        #region Singleton
        private readonly static object c_syncLockObject = new object();
        private volatile static NoFolder c_singleton;

        private NoFolder()
        {
        }

        public static NoFolder Instance
        {
            get
            {
                if (null == c_singleton)
                {
                    lock (c_syncLockObject)
                    {
                        if (null == c_singleton)
                        {
                            c_singleton = new NoFolder();
                        }
                    }
                }
                return c_singleton;
            }
        }

        #endregion

        public string Name => "Folder not found; no name to show.";

        public string Path => "Folder not found; no path to show.";

    }
}