using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class Users : EmsBaseEntity
    {
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public long DiscordId { get; set; }
        public string HexId { get; set; }
        public int RankId { get; set; }
        public int JobId { get; set; }
        public bool AccessManagementPanel { get; set; } = false;
    }
}
