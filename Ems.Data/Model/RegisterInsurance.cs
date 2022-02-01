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
        public short InsuranceId { get; set; }
        public short CreditsLeft { get; set; }
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddDays(14);
    }
}
