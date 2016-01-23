using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FileElm_Parent
    {

        public DirectoryInfo DirectoryInfo
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public bool IsWriteAccessible
        {
            get
            {
                this.TestWriteAccessOnce();

                return _IsWriteAccessible;
            }
        }
        bool _IsWriteAccessible = false;

        public bool WriteCheckDone
        {
            get { return _WriteCheckDone; }
        }
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

    }
}
