using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderOverride.Util;

namespace FolderOverride.ProcessElements
{
    class MainFolderMgr
    {
        public MainFolderMgr(string path)
        {
            DI_main = new DirectoryInfo(path);
            if (!DI_main.Exists)
                throw new InvalidDataException();
        }

        public DirectoryInfo DI_main;

        public void Prepare()
        {

            FolderElm_List = new List<FolderElm>(1000);
            FolderElm_List.Add(new FolderElm
            {
                DirectoryInfo = DI_main,
                IncludeSubFolders = true,
                Parent = null,
            });

            PrepareElements();
        }

        public void PrepareElements()
        {
            var t1 = DateTime.Now;

            //List<FolderElm> FolderInfo_List = new List<FolderElm>(
            //    //m_proc.ConfirmingPaths.FolderInfo_List);
            //    this.FolderInfo_List);

            FileElm_List = new List<FileElm>(100000);

            //foreach (FolderInfo foldInf in FolderInfo_List)
            for (int i = 0; i < FolderElm_List.Count; i++)
            {
                FolderElm folderElm = FolderElm_List[i];

                //if (foldInf.Status != FolderStatus.Ready)
                //    continue;

                if (folderElm.IncludeSubFolders)
                {
                    DirectoryInfo[] di_List = null;

                    try
                    {
                        di_List = folderElm.DirectoryInfo.GetDirectories(
                            //"*.*", SearchOption.AllDirectories);
                            "*.*", SearchOption.TopDirectoryOnly);
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (DirectoryInfo di in di_List)
                    {
                        FolderElm_List.Add(new FolderElm
                        {
                            DirectoryInfo = di,
                            IncludeSubFolders = true,
                            //Status = FolderStatus.Ready
                            Parent = folderElm,
                        });
                    }

                }

            }

            foreach (FolderElm folderElm in FolderElm_List)
            {
                FileInfo[] fiColl = null;

                try
                {
                    fiColl = folderElm.DirectoryInfo.GetFiles(
                        //"*.pdf", SearchOption.TopDirectoryOnly);
                        "*.*", SearchOption.TopDirectoryOnly);
                }
                catch
                {
                    continue;
                }

                foreach (FileInfo fi in fiColl)
                {
                    FileElm fiElm = new FileElm
                    {
                        FileInfo = fi,
                        ParentFolder = folderElm,
                    };

                    FileElm_List.Add(fiElm);
                }

                //if (fiColl.Length > 0)
                //    FileParent_List.Add(fe_Parent);
            }

            var t2 = DateTime.Now;
            var ts = t2.Subtract(t1);

            //var l1 = FileElm_List.Select(x => x.FileInfo.Length).ToList();

            //FileElm_List.OrderBy(x => x.FileInfo.Length)
            //l1.Sort();

            //Sort(x => x.FileInfo.Length);

            //this.FileElm_List.Sort((x, y) => x.DateModified < y.DateModified);

            this.FileElm_List.Sort(FileElm.SortComparison);
            //this.FileElm_List.Sort((x, y) =>
            //{
            //    long longVal1;

            //    longVal1 = x.Size - y.Size;
            //    if (longVal1 > 0)
            //        return (int)1;
            //    else if (longVal1 < 0)
            //        return (int)-1;

            //    int intVal1 = x.DateModified.CompareTo(y.DateModified);

            //    return intVal1;
            //});

            var list11 = this.FileElm_List.Select(x => new 
            { 
                x.Size, 
                x.DateModified,
                x.Name,
            }).ToList();


            int a;
            for(int i=1; i < this.FileElm_List.Count; i++)
            {
                var e1 = this.FileElm_List[i];
                var e2 = this.FileElm_List[i-1];

                //var c1 = FileElm.SortComparison(e1, e2);

                //if (c1 == 0)
                //{
                //    a = 0;

                //}
            
            }

        }


        public List<FileElm> FileElm_List
        {
            get;
            set;
        }

        public List<FolderElm> FolderElm_List
        {
            get;
            set;
        }

    }

}
