using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model
{
    public class MenuPermissions : EmsBaseEntity
    {
        public int MainMenuId { get; set; }
        public int SubMenuId { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }
        public int? RankId { get; set; }
    }
}
