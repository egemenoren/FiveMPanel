using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ems.Data.Data
{
    public class EmsContext : DbContext
    {
        public EmsContext():base("DiscordNET")
        {

        }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<RegisterInsurance> RegisterInsurance { get; set; }
        public DbSet<Processes> Processes { get; set; }
        public DbSet<Ranks> Ranks { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
    }
}
