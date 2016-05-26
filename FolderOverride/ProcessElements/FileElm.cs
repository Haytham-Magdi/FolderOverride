using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FileElm
    {
        public FileElm()
        {
            //Should_RemOrg = true;

            ////Status = FileStatus.Unknown;
            //Status = FileStatus.Ready;

            //MovingAction = FileMovingAction.Unknown;

            //CoreFile_ID = -1;

            //StatusMsg = "";
        }

        public static Comparison<FileElm> SortComparison
        {
            get
            {
                return (x, y) =>
                {
                    long longVal1;

                    longVal1 = x.Size - y.Size;
                    if (longVal1 > 0)
                        return (int)1;
                    else if (longVal1 < 0)
                        return (int)-1;

                    int intVal1 = x.DateModified.CompareTo(y.DateModified);

                    if (intVal1 != 0)
                        return intVal1;

                    var boolVal1 = x.CompareUniqueNums(y);
                    if (!boolVal1)
                        //boolVal1 = boolVal1;
                        return 1;

                    return 0;
                };
            }
        }

        public static Comparison<FileElm> OverrideComparison
        {
            get
            {
                return (x, y) =>
                {
                    //long longVal1;

                    int intVal1;

                    intVal1 = FileElm.SortComparison(x, y);

                    if (intVal1 != 0)
                        return intVal1;

                    //bool boolVal1;

                    return 0;
                };
            }
        }

        public FileElm Repeated_FileElm
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }


        public string Name
        {
            get { return this.FileInfo.Name; }
        }

        public string FolderPath
        {
            get { return this.FileInfo.DirectoryName; }
        }

        public long Size
        {
            get { return this.FileInfo.Length; }
        }

        public DateTime DateModified
        {
            get { return this.FileInfo.LastWriteTime; }
        }

        //public bool Should_RemOrg_____3
        //{
        //    get { return this.FileInfo.Directory.GetAccessControl(); }
        //}

        public FileInfo FileInfo
        {
            get;
            set;
        }

        //public FileStatus Status
        //{
        //    get;
        //    set;
        //}

        //public FileMovingAction MovingAction
        //{
        //    get;
        //    set;
        //}


        //public bool IsFailed
        //{
        //    get;
        //    set;
        //}

        public string StatusMsg
        {
            get;
            set;
        }

        public FolderElm ParentFolder
        {
            get;
            set;
        }

        public bool CanWrite_To_ParentFolder
        {
            get
            {
                return this.ParentFolder.IsWriteAccessible;
            }
        }

        public bool Cannot_Write_To_ParentFolder
        {
            get
            {
                return !(this.ParentFolder.IsWriteAccessible);
            }
        }


        public bool DeleteSourceFile_AfterCapture
        {
            get
            {
                if (this.CanWrite_To_ParentFolder == false)
                    return false;
                else
                    return _DeleteSourceFile_AfterCapture;
            }
            set
            {
                if (value && !this.CanWrite_To_ParentFolder)
                    throw new InvalidOperationException();

                _DeleteSourceFile_AfterCapture = value;
            }
        }
        bool _DeleteSourceFile_AfterCapture = false;
        //bool _DeleteSourceFile_AfterCapture = true;


        public bool WasCoreMissing
        {
            get;
            set;
        }


        public bool WriteMarkFile_To_SourceFolder
        {
            get
            {
                if (this.CanWrite_To_ParentFolder == false)
                    return false;
                else
                    return _WriteMarkFile_To_SourceFolder;
            }
            set
            {
                if (this.CanWrite_To_ParentFolder == false)
                    _WriteMarkFile_To_SourceFolder = false;
                else
                    _WriteMarkFile_To_SourceFolder = value;
            }
        }
        bool _WriteMarkFile_To_SourceFolder = true;

        public bool CompareUniqueNums(FileElm fileElm)
        {
            this.PrepareUniqueNumsOnce();
            fileElm.PrepareUniqueNumsOnce();

            for (int i = 0; i < this._UniqueArr.Length; i++)
            {
                if (this._UniqueArr[i] != fileElm._UniqueArr[i])
                    return false;
            }

            return true;
        }

        bool _UniqueNumsReady = false;

        byte[] _UniqueArr;
        //int _UniqueDataLen = 0;

        public void PrepareUniqueNumsOnce()
        {
            if (_UniqueNumsReady)
                return;

            try
            {
                byte[] readBuf;
                {
                    //const int nBufSiz_0 = 1000000;
                    const int nBufSiz_0 = 10000;
                    //const int nBufSiz_0 = 100;

                    const int nBufSiz = nBufSiz_0 - (nBufSiz_0 % 32);

                    //byte[] uniqueArr = new byte[32];
                    _UniqueArr = new byte[32];


                    for (int i = 0; i < _UniqueArr.Length; i++)
                        _UniqueArr[i] = 0;

                    readBuf = new byte[nBufSiz];
                }

                FileStream fs = this.FileInfo.OpenRead();


                int nReadCnt = fs.Read(readBuf, 0, readBuf.Length);

                while (nReadCnt > 0)
                {
                    int nRemCnt = (32 - nReadCnt % 32) % 32;

                    for (int i = 0; i < nRemCnt; i++)
                        readBuf[nReadCnt + i] = 0;

                    int nofCycles = nReadCnt / 32;
                    if (nReadCnt % 32 > 0)
                        nofCycles++;

                    for (int i = 0; i < nofCycles; i++)
                    {
                        int nBgn = i * 32;

                        for (int j = 0; j < 32; j++)
                        {
                            _UniqueArr[j] ^= readBuf[nBgn + j];
                        }
                    }

                    nReadCnt = fs.Read(readBuf, 0, readBuf.Length);
                }   //  while

                _UniqueNumsReady = true;
            }
            catch (Exception exp)
            {
                //this.Status = FileStatus.Failed;
                this.StatusMsg = exp.Message;
            }
        }

    }

}
