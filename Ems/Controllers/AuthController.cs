﻿using Ems.Service.Logger;
using Ems.Service.Management;
using Ems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ems.Controllers
{
    public class AuthController : BaseController
    {
        private UserManager userManager;
        private JobManager jobManager;
        private RankManager rankManager;
        public AuthController()
        {
            userManager = new UserManager();
            rankManager = new RankManager();
            jobManager = new JobManager();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            try
            {
                var entity = userManager.GetByParameter(x => x.Mail == Email && x.Password == Password && x.Status == Data.Model.Status.Active);
                if (entity != null)
                {
                    AddSession(Email);
                    LogManager.GetInstance().Info(entity.Id, entity.Mail, Email + " " + Password, "Successfully Logged In", "", Request.Url.AbsoluteUri, Data.Model.Log.LogType.Login);
                }
                else
                {
                    LogManager.GetInstance().Warning(null, null, Email + " " + Password, "Wrong Password or Email", "", Request.Url.AbsoluteUri, Data.Model.Log.LogType.Login);
                }
                if (Session["User"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                LogManager.GetInstance().Error(null, null, Email + " " + Password, ex.Message, ex.StackTrace, Request.Url.AbsoluteUri, Data.Model.Log.LogType.Login);
            }
            
            return RedirectToAction("Index", "Home");
        }
        internal void AddSession(string Email)
        {
            userManager = new UserManager();
            var userName = userManager.GetAllByParameter(x => x.Mail == Email).FirstOrDefault();
            if (userName != null)
            {
                rankManager = new RankManager();
                jobManager = new JobManager();
                MenusManager menusManager = new MenusManager();
                MainMenusManager mainMenusManager = new MainMenusManager();
                SubMenusManager subMenusManager = new SubMenusManager();
                List<SubMenusViewModel> menus = new List<SubMenusViewModel>();

                var permissions = menusManager.GetPermissions(userName.Id,userName.RankId,userName.JobId);
                

                foreach (var item in permissions)
                {
                    
                    var menuName = mainMenusManager.GetById(item.MainMenuId).MenuName;
                    var visibleSubMenus = subMenusManager.GetByParameter(x => x.Id == item.SubMenuId);
                    if (visibleSubMenus == null)
                        continue;
                    menus.Add(new SubMenusViewModel
                    {
                        Action = visibleSubMenus.Action,
                        Controller = visibleSubMenus.Controller,
                        MainMenuId = item.MainMenuId,
                        SubMenuId = item.Id,
                        SubMenuName = visibleSubMenus.Name,
                        MainMenuName = menuName
                    });
                }
                Session["Menus"] = menus;
                Session["Email"] = userName.Mail;
                userSession = userName;
                FormsAuthentication.SetAuthCookie(userName.NameSurname, true);
            }

        }
        public ActionResult Logout()
        {
            var Email = Session["Email"] ?? null;
            try
            {
                var user = userManager.GetByParameter(x => x.Mail == Email);
                FormsAuthentication.SignOut();
                Session.Abandon();
                userSession = null;
                LogManager.GetInstance().Info(user.Id, Email.ToString(), null, "Logged Out User", "", Request.Url.AbsoluteUri, Data.Model.Log.LogType.Logout);

            }
            catch (Exception ex)
            {
                LogManager.GetInstance().Error(null, Email.ToString(), null, ex.Message, ex.StackTrace, Request.Url.AbsoluteUri, Data.Model.Log.LogType.Logout);
            }
            return RedirectToAction("Login");

        }
    }
}