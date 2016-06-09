using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class DestApplier
    {
        //public static void Proceed(IEnumerable<FolderDestAction> folderDestActions, IEnumerable<FileDestAction> fileDestActions)
        public static void Proceed(DestPlan destPlan)
        {
            //  Validate folder actions.
            foreach (FolderDestAction folderAction in destPlan.FolderDestActions)
            {
                ValidateFolderAction(folderAction);
            }

            //  Validate file actions.
            foreach (FileDestAction fileAction in destPlan.FileDestActions)
            {
                ValidateFileAction(fileAction);
            }

            ExecuteCreateFolders(destPlan);

            //  execute file actions.
            foreach (FileDestAction fileAction in destPlan.FileDestActions)
            {
                ExecuteFileAction(fileAction);
            }

            //  Delete folders not in source.
            ExecuteDeleteFolders(destPlan);

        }

        private static void ExecuteDeleteFolders(DestPlan destPlan)
        {
            foreach (FolderDestAction folderAction in destPlan.FolderDestActions)
            {
                if (folderAction.Type == FolderDestAction.ActionType.Delete)
                {
                    DirectoryInfo di = new DirectoryInfo(folderAction.DestFullName);
                    if (di.Exists)
                    {
                        di.Delete(true);
                    }
                }
            }
        }

        private static void ExecuteCreateFolders(DestPlan destPlan)
        {
            foreach (FolderDestAction folderAction in destPlan.FolderDestActions)
            {
                if (folderAction.Type == FolderDestAction.ActionType.Create)
                {
                    DirectoryInfo di = new DirectoryInfo(folderAction.DestFullName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                }
            }

        }

        protected static void ValidateFolderAction(FolderDestAction folderAction)
        {
            DirectoryInfo di = new DirectoryInfo(folderAction.DestFullName);

            switch (folderAction.Type)
            {
                case FolderDestAction.ActionType.Create:
                    if (di.Exists)
                    {
                        //throw new InvalidOperationException();
                        return;
                    }
                    break;

                case FolderDestAction.ActionType.Delete:
                    if (!di.Exists)
                    {
                        //throw new InvalidOperationException();
                        return;
                    }
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        protected static void ValidateFileAction(FileDestAction fileDestAction)
        {
            FileInfo srcFi = fileDestAction.FileInfo;
            FileInfo destFi = new FileInfo(fileDestAction.DestFullName);

            switch (fileDestAction.Type)
            {
                case FileDestAction.ActionType.Copy:
                    if (!srcFi.Exists || destFi.Exists)
                    {
                        throw new InvalidOperationException();
                    }
                    break;
                //case FileDestAction.ActionType.Move:
                //    ExecuteMove();
                //    break;
                //case FileDestAction.ActionType.Delete:
                //    ExecuteDelet();
                //    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        protected static void ExecuteFileAction(FileDestAction fileAction)
        {
            FileInfo srcFi = fileAction.FileInfo;
            FileInfo destFi = new FileInfo(fileAction.DestFullName);

            switch (fileAction.Type)
            {
                case FileDestAction.ActionType.Copy:
                    if (!srcFi.Exists || destFi.Exists)
                    {
                        throw new InvalidOperationException();
                    }
                    Util.CommonUtil.Safe_CopyFileTo(fileAction.FileInfo, fileAction.DestFullName);
                    break;
                //case FileDestAction.ActionType.Move:
                //    ExecuteMove();
                //    break;
                //case FileDestAction.ActionType.Delete:
                //    ExecuteDelet();
                //    break;
                default:
                    throw new InvalidOperationException();
            }
        }

    }
}
