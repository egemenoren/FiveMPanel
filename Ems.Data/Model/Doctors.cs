using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Doctors : EmsBaseEntity
    {
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public ulong DiscordId { get; set; }
        public int RankId { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
}
