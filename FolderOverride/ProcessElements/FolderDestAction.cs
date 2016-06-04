using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FolderDestAction
    {
        public enum ActionType
        {
            Create = 0,
            Delete,
        }

        //public DirectoryInfo DirectoryInfo { get; set; }
        public ActionType Type { get; set; }
        public string DestFullName { get; set; }
    }
}
