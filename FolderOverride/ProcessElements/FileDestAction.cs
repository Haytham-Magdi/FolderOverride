using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class FileDestAction
    {
        public enum ActionType
        {
            Copy = 0,
            Move,
            Delete,
        }

        public FileInfo FileInfo { get; set; }
        public ActionType Type { get; set; }
        public string DestFullName { get; set; }

        public void Execute()
        {
        }

        void ExecuteCopy()
        {
            this.FileInfo.CopyTo(this.DestFullName);
        }

        void ExecuteMove()
        {
            throw new NotImplementedException();
        }

        void ExecuteDelet()
        {
            throw new NotImplementedException();
        }
    }
}
