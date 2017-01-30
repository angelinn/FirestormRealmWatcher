using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmWatcher
{
    public class RealmStatus
    {
        public string Status { get; set; }
        public string Since { get; set; }
        public bool AfterCrash { get; set; } = true;
    }    
}

