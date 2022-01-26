using Ems.Data.Model;
using Ems.Service.Logger;
using Ems.Service.Management;
using Ems.Service.Utility;
using Ems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Controllers
{
    public class ApplicationController : BaseController
    {
        private MainMenusManager mainMenusManager;
        private SubMenusManager subMenusManager;
        public ApplicationController()
        {
            mainMenusManager = new MainMenusManager();
            subMenusManager = new SubMenusManager();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetMainMenus()
        {
            var menus = mainMenusManager.GetAll();
            return Json(menus, JsonRequestBehavior.AllowGet);
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
                LogManager.GetInstance().Info(userSession.Id,userSession.Mail,parameters,null,null,Request.Url.AbsolutePath,Data.Model.Log.LogType.Insert);
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
                if(subMenusManager.GetByParameter(x=>x.Name == model.Name) != null)
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