using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FolderInfo
    {
        public FolderInfo()
        {
            IncludeSubFolders = true;
            //Status = FolderStatus.Unknown;
        }


        public string Path
        {
            get;
            set;
        }

        public bool IncludeSubFolders
        {
            get;
            set;
        }

        //public FolderStatus Status
        //{
        //    get;
        //    set;
        //}

        public DirectoryInfo DirectoryInfo
        {
            get;
            set;
        }
    }
}
