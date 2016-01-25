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
            _di_main = new DirectoryInfo(path);
            if (!_di_main.Exists)
                throw new InvalidDataException();
        }

        DirectoryInfo _di_main;

        public void Prepare()
        {

            FolderInfo_List = new List<FolderInfo>(1000);
            FolderInfo_List.Add(new FolderInfo
            {
                DirectoryInfo = _di_main,
                IncludeSubFolders = true,
                Path = _di_main.FullName,
            });

            PrepareFileElms();
        }

        public void PrepareFileElms()
        {
            var t1 = DateTime.Now;

            List<FolderInfo> foldInf_List = new List<FolderInfo>(
                //m_proc.ConfirmingPaths.FolderInfo_List);
                this.FolderInfo_List);

            FileElm_List = new List<FileElm>(100000);

            FileParent_List = new List<FileElm_Parent>(100000);


            List<FileElm_Parent> fe_Parent_List =
                new List<FileElm_Parent>(10000);


            //foreach (FolderInfo foldInf in foldInf_List)
            for (int i = 0; i < foldInf_List.Count; i++)
            {
                FolderInfo foldInf = foldInf_List[i];

                //if (foldInf.Status != FolderStatus.Ready)
                //    continue;

                fe_Parent_List.Add(new FileElm_Parent
                {
                    DirectoryInfo = foldInf.DirectoryInfo
                });

                if (foldInf.IncludeSubFolders)
                {
                    DirectoryInfo[] di_List = null;

                    try
                    {
                        di_List = foldInf.DirectoryInfo.GetDirectories(
                            //"*.*", SearchOption.AllDirectories);
                            "*.*", SearchOption.TopDirectoryOnly);
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (DirectoryInfo di in di_List)
                    {
                        //fe_Parent_List.Add(new FileElm_Parent
                        //{
                        //    DirectoryInfo = di
                        //});

                        foldInf_List.Add(new FolderInfo
                        {
                            DirectoryInfo = di,
                            Path = di.FullName,
                            IncludeSubFolders = true,
                            //Status = FolderStatus.Ready
                        });
                    }

                }

            }

            foreach (FileElm_Parent fe_Parent in fe_Parent_List)
            {
                FileInfo[] fiColl = null;

                try
                {
                    fiColl = fe_Parent.DirectoryInfo.GetFiles(
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
                        ParentFolder = fe_Parent
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

            this.FileElm_List.Sort(FileElm.Comparison);
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
                x.DateModified ,
                x.Name,
            }).ToList();


            int a;
            for(int i=1; i < this.FileElm_List.Count; i++)
            {
                var e1 = this.FileElm_List[i];
                var e2 = this.FileElm_List[i-1];

                //var c1 = FileElm.Comparison(e1, e2);

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

        public List<FileElm_Parent> FileParent_List
        {
            get;
            set;
        }

        public List<FolderInfo> FolderInfo_List
        {
            get;
            set;
        }

    }

}
