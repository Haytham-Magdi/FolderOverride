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

            DestApplier.Proceed(_destPlan);

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

            var srcFiles = this._srcMain.FileElm_List;
            var destFiles = this._destMain.FileElm_List;

            var tmpIdx = 100;
            var dif1 = FileElm.BasicComparison(srcFiles[0 + tmpIdx], srcFiles[1 + tmpIdx]); //  -1
            var dif2 = FileElm.BasicComparison(srcFiles[1 + tmpIdx], srcFiles[2 + tmpIdx]); //  -1
            var dif_2 = FileElm.BasicComparison(srcFiles[2 + tmpIdx], srcFiles[1 + tmpIdx]);    //  1

            var list_destBasicEquals = new List<FileElm>();

            int srcIdx = 0;
            int destIdx = 0;
            while (srcIdx != srcFiles.Count() || destIdx != destFiles.Count())
            {
                if (srcIdx != srcFiles.Count() && destIdx == destFiles.Count())
                {
                    var srcFile = srcFiles[srcIdx];
                    FileDestAction fileAction = CreateFileCopyAction(srcFile);
                    list_FileDestActions.Add(fileAction);
                    srcIdx++;
                    continue;
                }
                else if (srcIdx == srcFiles.Count() && destIdx != destFiles.Count())
                {
                    var destFile = destFiles[destIdx];
                    FileDestAction fileAction = CreateFileDeleteAction(destFile);
                    list_FileDestActions.Add(fileAction);
                    destIdx++;
                    continue;
                }
                else
                {
                    var srcFile = srcFiles[srcIdx];
                    var destFile = destFiles[destIdx];

                    if (srcFile.RelativeFullName == "\\IbsHaythamMagdiTask.v12.suo")
                        srcIdx = srcIdx;

                    var basicComp = FileElm.BasicComparison(srcFile, destFile);

                    if (basicComp == 0)
                    {
                        list_destBasicEquals.Clear();
                        var basicComp_2 = basicComp;
                        var fullNameComp = FileElm.FullNameComparison(srcFile, destFile);
                        //for (int k = destIdx; k < destFiles.Count && fullNameComp != 0 && basicComp_2 == 0; k++)
                        for (int k = destIdx; k < destFiles.Count && fullNameComp != 0 && basicComp_2 == 0; k++)
                        {
                            list_destBasicEquals.Add(destFiles[k]);

                            fullNameComp = FileElm.FullNameComparison(srcFile, destFile);

                            basicComp_2 = FileElm.BasicComparison(srcFile, destFile);

                            throw new NotImplementedException();
                        }

                        throw new NotImplementedException();


                        if (srcFile.RelativeFullName != destFile.RelativeFullName)
                        {
                            FileDestAction fileAction = CreateFileMoveAction(srcFile, destFile);
                            list_FileDestActions.Add(fileAction);
                        }

                        srcIdx++;
                        destIdx++;
                        continue;
                    }
                    else if (basicComp < 0)
                    {
                        FileDestAction fileAction = CreateFileCopyAction(srcFile);
                        list_FileDestActions.Add(fileAction);
                        srcIdx++;
                        continue;
                    }
                    else if (basicComp > 0)
                    {
                        FileDestAction fileAction = CreateFileDeleteAction(destFile);
                        list_FileDestActions.Add(fileAction);
                        destIdx++;
                        continue;
                    }
                }
            }

            var destPlan = new DestPlan
            {
                FolderDestActions = list_FolderDestActions,
                FileDestActions = list_FileDestActions,
            };

            #endregion

            _destPlan = destPlan;
        }

        private static FileDestAction CreateFileDeleteAction(FileElm destFile)
        {
            return new FileDestAction
            {
                Type = FileDestAction.ActionType.Delete,
                FileInfo = destFile.FileInfo,
            };
        }

        private FileDestAction CreateFileCopyAction(FileElm srcFile)
        {
            return new FileDestAction
            {
                Type = FileDestAction.ActionType.Copy,
                FileInfo = srcFile.FileInfo,
                DestFullName = this._destMain.DI_main.FullName + srcFile.RelativeFullName,
            };
        }

        private FileDestAction CreateFileMoveAction(FileElm srcFile, FileElm destFile)
        {
            return new FileDestAction
            {
                Type = FileDestAction.ActionType.Move,
                FileInfo = destFile.FileInfo,
                DestFullName = this._destMain.DI_main.FullName + srcFile.RelativeFullName,
            };
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
