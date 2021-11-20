using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Hierarchy:EmsBaseEntity
    {
        public int JobId { get; set; }
        public short HierarchyRank { get; set; }
        public int RankId { get; set; }
    }
}
