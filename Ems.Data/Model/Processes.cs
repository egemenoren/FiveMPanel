using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Processes : EmsBaseEntity
    {
        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public int Price { get; set; }
    }
}
