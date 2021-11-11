using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class RegisterInsurance : EmsBaseEntity
    {
        public string NameSurname { get; set; }
        public int DoctorId { get; set; }
        public int InsuranceId { get; set; }
    }
}
