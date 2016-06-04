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
        public static void Proceed(IEnumerable<FolderDestAction> folderDestActions, IEnumerable<FileDestAction> fileDestActions)
        {
            foreach(FolderDestAction folderAction in folderDestActions)
            {


            }

        }

        protected static void ExecuteFolderAction(FolderDestAction folderAction)
        {
            DirectoryInfo di = new DirectoryInfo(folderAction.DestFullName);

            switch(folderAction.Type)
            {
                case FolderDestAction.ActionType.Create:
                    if(di.Exists)
                    {
                        throw new InvalidOperationException();
                    }
                    di.Create();
                    break;

                case FolderDestAction.ActionType.Delete:
                    if(!di.Exists)
                    {
                        throw new InvalidOperationException();
                    }
                    di.Delete();
                    break;

                default:
                    throw new InvalidOperationException();

            }
        }
    
    }
}
