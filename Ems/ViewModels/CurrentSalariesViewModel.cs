using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class CurrentSalariesViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public double CurrentSalary { get; set; }
        public double TotalUntilCurrent { get; set; }
        public int ExaminationCount { get; set; }
        public int RegisterInsuranceCount { get; set; }
    }
}