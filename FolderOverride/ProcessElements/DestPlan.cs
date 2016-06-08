using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderOverride.ProcessElements
{
    public class DestPlan
    {
        public IEnumerable<FileDestAction> FileDestActions;
        public IEnumerable<FolderDestAction> FolderDestActions;
    }
}
