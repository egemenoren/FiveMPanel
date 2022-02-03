using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class EmsBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Fix { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public Status Status {get;set;} = Status.Active;
    }
}
