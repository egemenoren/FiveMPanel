using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Interventions:EmsBaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
