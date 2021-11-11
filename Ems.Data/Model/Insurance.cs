using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Insurance : EmsBaseEntity
    {
        public string PackageName { get; set; }
        public double DiscountRateForProcesses { get; set; }
        public double DiscountRateForTests { get; set; }
        public double DiscountRateForExtras { get; set; }
        public int Price { get; set; }
    }
}
