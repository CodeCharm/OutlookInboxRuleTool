using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Office.Interop.Outlook;

namespace CodeCharm.OutlookInterop
{
    public class Folder
        : IFolder
    {
        private readonly MAPIFolder _folder;

        internal Folder(MAPIFolder folder)
        {
            _folder = folder ?? throw new ArgumentNullException(nameof(folder));
        }

        public string Name { get { return _folder.Name; } }

        public string Path { get { return _folder.FullFolderPath; } }
    }
}
