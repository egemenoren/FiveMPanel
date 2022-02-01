using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class AddExaminationViewModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string NameSurname { get; set; }
        public string Diagnosis { get; set; }
        public int ProcessId { get; set; }
        public double Price { get; set; }
        public bool IsJudical { get; set; }
        public string OfficerNameSurname { get; set; }
        public bool UseInsurance { get; set; }
        public bool? HasInsurance { get; set; }
        public short? InsuranceId { get; set; }
        public short? InsuranceCredit { get; set; }
        public bool UsedDrugs { get; set; }
        public string Extras { get; set; }
    }
}