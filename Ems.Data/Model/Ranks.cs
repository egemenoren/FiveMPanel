using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Ranks : EmsBaseEntity
    {
        public int JobId { get; set; }
        public string RankName { get; set; }
        public bool AccessJobPanel { get; set; }
        public byte HierarchyNo { get; set; }
        public int HourlySalary { get; set; }
    }
}
