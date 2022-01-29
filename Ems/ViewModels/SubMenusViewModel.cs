using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class SubMenusViewModel
    {
        public int  MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Icon { get; set; }
        public int? UserId { get; set; }
        public int? RankId { get; set; }
        public int? JobId { get; set; }
        public bool? HasPermission { get; set; }
        public short? DisplayNo { get; set; }
        public bool? Display { get; set; }
    }
}