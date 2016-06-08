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

            {
                List<FolderDestAction> list_FolderDestActions = new List<FolderDestAction>();
                foreach (FolderElm folderElm in this._srcMain.FolderElm_List)
                {
                    if (folderElm.RelativePath == "\\")
                        continue;

                    var folderAction = new FolderDestAction
                    {
                        DestFullName = this._destMain.DI_main.FullName + folderElm.RelativePath,
                        Type = FolderDestAction.ActionType.Create,
                    };
                    list_FolderDestActions.Add(folderAction);
                }

                List<FileDestAction> list_FileDestActions = new List<FileDestAction>();

                var destPlan = new DestPlan
                {
                    FolderDestActions = list_FolderDestActions,
                    FileDestActions = list_FileDestActions,
                };

                DestApplier.Proceed(destPlan);
            }
            int a;
            {
                var di1 = new DirectoryInfo(
                    @"E:\HthmWork\Home-Projects\FolderOverride-Stuff\TestFolders\NewDestParent\NewDest");

                var ex1 = di1.Exists;

                if (!di1.Exists)
                {
                    di1.Create();
                }

                //var stack_NotExists = new Stack<DirectoryInfo>();

                //var di2 = di1;
                //while (!di2.Exists)
                //{
                //    stack_NotExists.Push(di2);
                //}

                //while (stack_NotExists.Count() > 0)
                //{
                //    di2 = stack_NotExists.Pop();

                //    di2.Create();
                //}


                a = 0;
            }

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
