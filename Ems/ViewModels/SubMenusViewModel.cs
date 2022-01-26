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
    }
}