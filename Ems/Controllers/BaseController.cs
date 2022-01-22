using Ems.Service.Management;
using Ems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Ems.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var Email = Session["Email"] ?? null;
            

            
            if(Email != null)
            {
                UserManager userManager = new UserManager();
                var userName = userManager.GetByParameter(x => x.Mail == Email);
                MenusManager menusManager = new MenusManager();
                MainMenusManager mainMenusManager = new MainMenusManager();
                List<SubMenusViewModel> menus = new List<SubMenusViewModel>();
                var permissions = menusManager.GetPermissions(userName.Id);
                foreach (var item in permissions)
                {
                    var menuName = mainMenusManager.GetById(item.MainMenuId).MenuName;
                    menus.Add(new SubMenusViewModel
                    {
                        Action = item.Action,
                        Controller = item.Controller,
                        MainMenuId = item.MainMenuId,
                        SubMenuId = item.Id,
                        SubMenuName = item.SubMenu,
                        MainMenuName = menuName
                    });
                }
                Session["Menus"] = menus;
            }
            
        }
        internal bool CheckAccessForJobs(string JobName,bool Manager,string UserJobName)
        {
            if (Manager || JobName == UserJobName)
            {
                return true;
            }
            else
                return false;
        }

    }
}