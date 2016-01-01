using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FolderOverrideMgr
    {
        public FolderOverrideMgr(string path_srcFolder, string path_destFolder)
        {
            _srcMain = new MainFolderMgr(path_srcFolder);
            _destMain = new MainFolderMgr(path_destFolder);
        }

        MainFolderMgr _srcMain;
        MainFolderMgr _destMain;


        public void Proceed()
        {
            Prepare();


        }

        public void Prepare()
        {
            if (_isPrepared)
                return;

            _srcMain.Prepare();
            _destMain.Prepare();

            _isPrepared = true;
        }
        bool _isPrepared = false;



    }
}
