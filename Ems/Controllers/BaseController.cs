﻿using Ems.Service.Logger;
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
            var url = requestContext.HttpContext.Request.Url.AbsoluteUri;
            SideMenu(url);
            CheckPermission(requestContext);
            LogUrl(url);
            
            
        }
        private void LogUrl(string url)
        {
            try
            {
                UserManager userManager = new UserManager();
                var Email = Session["Email"] ?? null;
                if (Email != null)
                {
                    int userId = userManager.GetByParameter(x => x.Mail == Email).Id;
                    LogManager.GetInstance().Info(userId, Email.ToString(), null, null, null, url, Data.Model.Log.LogType.Generic);
                }
            }
            catch (Exception ex)
            {

            }
            
            
        }
        private void CheckPermission(RequestContext context)
        {

            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            try
            {
                var Email = Session["Email"] ?? null;
                UserManager userManager = new UserManager();
                MenusManager menusManager = new MenusManager();
                var user = userManager.GetByParameter(x => x.Mail == Email);
                var menu = menusManager.GetAllByParameter(x => x.Action == actionName && x.Controller == controllerName);
                bool userHasAccess = menu.Where(x => x.UserId == user.Id).Count() > 0 ? true : false;
                if (!userHasAccess && controllerName != "Base")
                     new RedirectResult(Url.Action("Index", "Home")).ExecuteResult(this.ControllerContext);
                    
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
            filterContext.ExceptionHandled = true;
        }
        [ChildActionOnly]
        public ActionResult SideMenu(string url)
        {
            var Email = Session["Email"] ?? null;
            List<SubMenusViewModel> menus = new List<SubMenusViewModel>();
            if (Email != null)
            {
                UserManager userManager = new UserManager();
                var userName = userManager.GetByParameter(x => x.Mail == Email);
                try
                {
                    MenusManager menusManager = new MenusManager();
                    MainMenusManager mainMenusManager = new MainMenusManager();
                    
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
                catch (Exception ex)
                {
                    LogManager.GetInstance().Error(userName.Id, Email.ToString(), null, ex.Message, ex.StackTrace, url, Data.Model.Log.LogType.Generic);
                }
                

            }
            return PartialView("_MenuPartial", menus);
        }

    }
}