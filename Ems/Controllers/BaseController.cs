using Ems.Data.Model;
using Ems.Service.Logger;
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
        public static Users userSession;
        public BaseController()
        {
            
        }
        protected override void Initialize(RequestContext requestContext)
        { 
            base.Initialize(requestContext);
            var url = requestContext.HttpContext.Request.Url.AbsoluteUri;
            //SideMenu(url);
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
                LogManager.GetInstance().Error(null, null, null, ex.Message, ex.StackTrace, url, Data.Model.Log.LogType.Generic);
            }
            
            
        }
        private void CheckPermission(RequestContext context)
        {
            
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString().ToLower();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            var Email = Session["Email"] ?? null;
            UserManager userManager = new UserManager();
            SubMenusManager subMenusManager = new SubMenusManager();
            MenusManager menus = new MenusManager();
            if(Email != null)
            {
                try
                {
                    var user = userManager.GetByParameter(x => x.Mail == Email);
                    if (userSession == null)
                        userSession = user;
                    var menu = subMenusManager.GetByParameter(x => x.Action == actionName && x.Controller == controllerName);
                    var menuExists = menu != null ? true:false;
                    var userHasSubmenu = menu != null ? menus.GetAllByParameter(x => x.UserId == user.Id && x.SubMenuId == menu.Id).Count() > 0 : false;
                    bool userHasAccess = (controllerName == "home" || controllerName == "base" || actionName == "login" || actionName == "logout")
                        ? true : userHasSubmenu; //(menu.Where(x => x.UserId == user.Id).Count() > 0 ? true : false);
                    //Hem erişimi yok hem de üçünden biri değil hem de management permi yok.
                    if (userHasAccess == false && user.AccessManagementPanel == false)
                        new RedirectResult(Url.Action("Index", "Home")).ExecuteResult(this.ControllerContext);
                    else
                    {
                        MenusManager menusManager = new MenusManager();
                        MainMenusManager mainMenusManager = new MainMenusManager();
                        var permissions = new List<MenuPermissions>();
                        if(user.AccessManagementPanel != true)
                            permissions = menusManager.GetPermissions(user.Id, user.RankId, user.JobId);
                        else
                        {
                            var entity = subMenusManager.GetAll();
                            foreach (var item in entity)
                            {
                                permissions.Add(new MenuPermissions
                                {
                                    MainMenuId = item.MainMenuId,
                                    SubMenuId = item.Id
                                });
                            }
                        }
                        List<SubMenusViewModel> menuList = new List<SubMenusViewModel>();
                        foreach (var item in permissions)
                        {
                            var userMenu = subMenusManager.GetByParameter(x => x.Id == item.SubMenuId);
                            var menuName = mainMenusManager.GetById(item.MainMenuId).MenuName;
                            menuList.Add(new SubMenusViewModel
                            {
                                Action = userMenu.Action,
                                Controller = userMenu.Controller,
                                MainMenuId = item.MainMenuId,
                                SubMenuId = item.Id,
                                SubMenuName = userMenu.Name,
                                MainMenuName = menuName,
                                Icon = userMenu.Icon
                            });
                        }
                        Session["Menus"] = menuList;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetInstance().Error(userManager.GetByParameter(x => x.Mail == Email).Id, Email.ToString(), null, ex.Message, ex.StackTrace, context.HttpContext.Request.Url.AbsoluteUri, Data.Model.Log.LogType.Generic);

                }
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
        //public void SideMenu(string url)
        //{
        //    var Email = Session["Email"] ?? null;
        //    List<SubMenusViewModel> menus = new List<SubMenusViewModel>();
        //    if (Email != null)
        //    {
        //        UserManager userManager = new UserManager();
        //        var userName = userManager.GetByParameter(x => x.Mail == Email);
        //        try
        //        {
        //            MenusManager menusManager = new MenusManager();
        //            MainMenusManager mainMenusManager = new MainMenusManager();
                    
        //            var permissions = userName.AccessManagementPanel == true ? menusManager.GetAll() : menusManager.GetPermissions(userName.Id,userName.RankId,userName.JobId);
        //                foreach (var item in permissions)
        //                {
        //                    var menuName = mainMenusManager.GetById(item.MainMenuId).MenuName;
        //                    menus.Add(new SubMenusViewModel
        //                    {
        //                        Action = item.Action,
        //                        Controller = item.Controller,
        //                        MainMenuId = item.MainMenuId,
        //                        SubMenuId = item.Id,
        //                        SubMenuName = item.SubMenu,
        //                        MainMenuName = menuName,
        //                        Icon = item.Icon
        //                    });
        //                }
        //            Session["Menus"] = menus;
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            LogManager.GetInstance().Error(userName.Id, Email.ToString(), null, ex.Message, ex.StackTrace, url, Data.Model.Log.LogType.Generic);
        //        }
                

        //    }
        //}

    }
}