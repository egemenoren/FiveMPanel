using Ems.Data.Model;
using Ems.Service.Logger;
using Ems.Service.Management;
using Ems.Service.Utility;
using Ems.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        private MainMenusManager mainMenusManager;
        private SubMenusManager subMenusManager;
        private MenusManager menusManager;
        public ApplicationController()
        {
            mainMenusManager = new MainMenusManager();
            subMenusManager = new SubMenusManager();
            menusManager = new MenusManager();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PermissionManagement()
        {
            return View();
        }
        public ActionResult EditRankPermission(int id)
        {
            // Submenülerin arasında rank'ın sahip olduğu bir yetki varsa seçili gelecek.
            var submenus = subMenusManager.GetAll();
            var menuPermissions = menusManager.GetAllByParameter(x => x.RankId == id);
            List<SubMenusViewModel> subMenusViewModels = new List<SubMenusViewModel>();
            foreach (var item in submenus)
            {
                var hasPermission = menusManager.GetByParameter(x => x.SubMenuId == item.Id && x.RankId == id) != null;
                var itemMainMenu = mainMenusManager.GetByParameter(x => x.Id == item.MainMenuId);
                subMenusViewModels.Add(new SubMenusViewModel
                {
                    Action = item.Action,
                    Controller = item.Controller,
                    HasPermission = hasPermission,
                    SubMenuId = item.Id,
                    SubMenuName = item.Name,
                    MainMenuId = itemMainMenu.Id,
                    MainMenuName = itemMainMenu.MenuName,
                });
            }
            ViewBag.Menus = subMenusViewModels;
            return View();
        }
        public ActionResult EditPermissions()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult AddMainMenu()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddMainMenu(MainMenus model)
        {
            try
            {
                var parameters = String<MainMenus>.GetInstance().GetPropertiesAsString(model);
                if (mainMenusManager.GetByParameter(x => x.MenuName == model.MenuName) != null)
                {
                    throw new Exception("Böyle bir ana menü zaten var.");
                }
                mainMenusManager.Add(model);
                LogManager.GetInstance().Info(userSession.Id, userSession.Mail, parameters, null, null, Request.Url.AbsolutePath, Data.Model.Log.LogType.Insert);
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().Error(userSession.Id, userSession.Mail, null, ex.Message, ex.StackTrace, Request.Url.AbsolutePath, Data.Model.Log.LogType.Generic);
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddSubMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubMenu(SubMenus model)
        {
            try
            {
                var parameters = String<SubMenus>.GetInstance().GetPropertiesAsString(model);
                if (subMenusManager.GetByParameter(x => x.Name == model.Name && x.Controller == model.Controller) != null)
                {
                    throw new Exception("Böyle bir  submenu zaten var.");
                }
                subMenusManager.Add(model);
                LogManager.GetInstance().Info(userSession.Id, userSession.Mail, parameters, null, null, Request.Url.AbsolutePath, Data.Model.Log.LogType.Insert);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                LogManager.GetInstance().Error(userSession.Id, userSession.Mail, null, ex.Message, ex.StackTrace, Request.Url.AbsolutePath, Data.Model.Log.LogType.Generic);
            }
            return View();
        }
    }
}