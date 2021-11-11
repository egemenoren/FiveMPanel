using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Ranks : EmsBaseEntity
    {
        public string Job { get; set; }
        public string RankName { get; set; }
        public bool AccessJobPanel { get; set; }
        public bool AccessManagementPanel { get; set; }
    }
}
