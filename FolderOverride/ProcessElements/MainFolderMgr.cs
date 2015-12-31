using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    class MainFolderMgr
    {
        public MainFolderMgr(string path)
        {
            _di_main = new DirectoryInfo(path);
            if (!_di_main.Exists)
                throw new InvalidDataException();
        }

        DirectoryInfo _di_main;

        public void Prepare()
        {

        }
    
    }

}
