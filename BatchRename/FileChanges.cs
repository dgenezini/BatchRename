using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class FileChanges
    {
        [DisplayName("FileName")]
        public string FileName { get; set; }
        [DisplayName("New FileName")]
        public string NewFileName { get; set; }
    }
}
