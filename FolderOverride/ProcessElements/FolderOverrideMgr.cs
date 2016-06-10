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

        DestPlan _destPlan;


        public void Proceed()
        {
            Prepare();

            CreateDestPlan();

            //DestApplier.Proceed(_destPlan);

            int a;

        }

        private void CreateDestPlan()
        {
            #region Folder stuff.

            List<FolderDestAction> list_FolderDestActions = new List<FolderDestAction>();
            
            foreach (FolderElm folderElm in this._srcMain.FolderElm_List)
            {
                if (folderElm.RelativePath == "\\")
                    continue;

                var di_Dest = new DirectoryInfo(this._destMain.DI_main.FullName + folderElm.RelativePath);
                if (di_Dest.Exists)
                {
                    continue;
                }

                var folderAction = new FolderDestAction
                {
                    DestFullName = di_Dest.FullName,
                    Type = FolderDestAction.ActionType.Create,
                };
                list_FolderDestActions.Add(folderAction);
            }

            foreach (FolderElm folderElm in this._destMain.FolderElm_List)
            {
                if (folderElm.RelativePath == "\\")
                    continue;

                var di_Src = new DirectoryInfo(this._srcMain.DI_main.FullName + folderElm.RelativePath);
                if (di_Src.Exists)
                {
                    continue;
                }

                var folderAction = new FolderDestAction
                {
                    DestFullName = this._destMain.DI_main.FullName + folderElm.RelativePath,
                    Type = FolderDestAction.ActionType.Delete,
                };
                list_FolderDestActions.Add(folderAction);
            }

            #endregion


            #region File stuff.

            List<FileDestAction> list_FileDestActions = new List<FileDestAction>();

            var destPlan = new DestPlan
            {
                FolderDestActions = list_FolderDestActions,
                FileDestActions = list_FileDestActions,
            };

            #endregion

            _destPlan = destPlan;
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
