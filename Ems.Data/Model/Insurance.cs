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
        public double DiscountRateForBasicProcesses { get; set; }
        public double DiscountRateForMediumProcesses { get; set; }
        public double DiscountRateForCriticalProcesses { get; set; }
        public int Credits { get; set; }
        public double Price { get; set; }
    }
}
