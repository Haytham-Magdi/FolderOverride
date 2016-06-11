using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.Util
{
    public class CommonUtil
    {
        public static string LastSubstring(string a_sArg, int a_nSubLen)
        {
            string sEnd = a_sArg.Substring(
                a_sArg.Length - a_nSubLen, a_nSubLen);

            return sEnd;
        }

        public static void Safe_CopyFileTo_0(FileInfo a_fi, string a_sDestFullName,
            bool bOverwrite = false)
        {
            a_fi.CopyTo(a_sDestFullName, bOverwrite);

            int nCycleWait = 100;

            long nCntMs_Max = 20000 * a_fi.Length / 100000000;

            int nCntMs = 0;

            while (true)
            {
                //FileSystemWatcher fsw1 = new FileSystemWatcher(

                FileInfo fi_2 = new FileInfo(a_sDestFullName);

                nCntMs += nCycleWait;
                //nCntMs++;

                if (fi_2.Length == a_fi.Length)
                {
                    using (FileStream fs1 = new FileStream(
                        a_sDestFullName, FileMode.Open))
                    {
                        if (fs1.Length != a_fi.Length)
                        {
                            throw new Exception("Unable to copy file");
                        }
                    }

                    break;
                }

                //if (nCntMs > 7000)
                if (nCntMs > nCntMs_Max)
                    throw new Exception("Unable to copy file");

                System.Threading.Thread.Sleep(nCycleWait);
            }

        }

        public static void Safe_CopyFileTo(FileInfo a_fi, string a_sDestFullName,
            bool bOverwrite = false)
        {
            a_fi.CopyTo(a_sDestFullName, bOverwrite);

            int nCycleWait = 100;

            long nCntMs_Max = 20000 * a_fi.Length / 100000000;

            int nCntMs = 0;

            while (true)
            {
                //FileSystemWatcher fsw1 = new FileSystemWatcher(

                FileInfo fi_2 = new FileInfo(a_sDestFullName);

                nCntMs += nCycleWait;
                //nCntMs++;

                if (fi_2.Length == a_fi.Length)
                {
                    //using (FileStream fs1 = new FileStream(
                    //    a_sDestFullName, FileMode.Open))
                    //{
                    //    if (fs1.Length != a_fi.Length)
                    //    {
                    //        throw new Exception("Unable to copy file");
                    //    }
                    //}

                    break;
                }

                //if (nCntMs > 7000)
                if (nCntMs > nCntMs_Max)
                    throw new Exception("Unable to copy file");

                System.Threading.Thread.Sleep(nCycleWait);
            }

        }

        public static string Get_FileExtension(string a_sFullName)
        {
            char[] letr_Arr = a_sFullName.ToCharArray();

            int i = letr_Arr.Length - 1;

            for (; i >= 0; i--)
            {
                if (letr_Arr[i] == '.')
                    break;
            }

            if (i == -1)
                throw new InvalidDataException();

            string sRet = a_sFullName.Substring(
                i, a_sFullName.Length - i);

            return sRet;
        }

        public static string Set_FileExtension(string a_sFullName, string a_sNewExt)
        {
            char[] letr_Arr = a_sFullName.ToCharArray();

            int i = letr_Arr.Length - 1;

            for (; i >= 0; i--)
            {
                if (letr_Arr[i] == '.')
                    break;
            }

            if (i == -1)
                throw new InvalidDataException();

            string sRet =
                a_sFullName.Substring(0, i) + a_sNewExt;

            return sRet;
        }
    }
}
