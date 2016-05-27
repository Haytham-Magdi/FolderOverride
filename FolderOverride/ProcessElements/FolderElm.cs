using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FolderElm
    {
        public FolderElm()
        {
            IncludeSubFolders = true;
            //Status = FolderStatus.Unknown;
        }

        public string Path
        {
            get { return this.DirectoryInfo.FullName; }
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

        public FolderElm Parent { get; set; }

        public bool IsWriteAccessible
        {
            get
            {
                this.TestWriteAccessOnce();

                return _IsWriteAccessible;
            }
        }
        bool _IsWriteAccessible = false;

        bool _WriteCheckDone = false;
        public void TestWriteAccessOnce()
        {
            if (_WriteCheckDone)
                return;

            try
            {
                string sDestFullName = this.DirectoryInfo.FullName +
                    "\\" + "DocArchiveTest.test";

                using (FileStream fs = File.Create(sDestFullName, 1,
                    FileOptions.DeleteOnClose)) { }

                _IsWriteAccessible = true;
            }
            catch
            {
                _IsWriteAccessible = false;
            }
            finally
            {
                _WriteCheckDone = true;
            }

        }

        public bool IsOrigin { get { return this.Parent == null; } }
    }
}
