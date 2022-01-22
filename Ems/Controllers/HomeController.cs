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
    public class HomeController : BaseController
    {
        private UserManager userManager;
        private RankManager rankManager;
        private JobManager jobManager;
        public HomeController()
        {
            AddSession("sa");
        }
        [Authorize]
        public ActionResult Index()
        {

            HttpCookie reqCookies = Request.Cookies["userInfo"];
            ViewBag.JobId = reqCookies["JobId"].ToString() ?? "";
            ViewBag.RankId = reqCookies["RankId"].ToString() ?? "";
            ViewBag.NameSurname = reqCookies["NameSurname"].ToString() ?? "";
            ViewBag.RankName = reqCookies["RankName"].ToString() ?? "";
            ViewBag.AccessJobPanel = reqCookies["AccessJobPanel"].ToString() ?? "";
            ViewBag.AccessManagementPanel = reqCookies["AccessManagementPanel"].ToString() ?? "";
            ViewBag.JobName = reqCookies["JobName"].ToString() ?? "";
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var entity = userManager.GetByParameter(x => x.Mail == Email && x.Password == Password);
            if (entity != null)
                AddSession(Email);

            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
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

                HierarchyManager hierarchyManager = new HierarchyManager();
                Session["Email"] = userName.Mail;
                FormsAuthentication.SetAuthCookie(userName.NameSurname, false);
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["RankId"] = Server.UrlEncode(userName.RankId.ToString());
                userInfo["JobId"] = Server.UrlEncode(userName.JobId.ToString());
                var rankEntity = rankManager.GetAllByParameter(x => x.Id == userName.RankId).FirstOrDefault();
                var jobEntity = jobManager.GetAllByParameter(x => x.Id == userName.JobId).FirstOrDefault();
                var hierarchyEntity = rankEntity != null ? hierarchyManager.GetByParameter(x => x.RankId == rankEntity.Id) : null;
                userInfo["JobName"] = Server.UrlEncode( jobEntity != null ? jobEntity.JobName : "NULL");
                userInfo["RankName"] = Server.UrlEncode(rankEntity != null ? rankEntity.RankName : "NULL");
                userInfo["AccessJobPanel"] = Server.UrlEncode(rankEntity != null ? rankEntity.AccessJobPanel.ToString() : "NULL" );
                userInfo["AccessManagementPanel"] = Server.UrlEncode(userName.AccessManagementPanel.ToString());
                userInfo["NameSurname"] = Server.UrlEncode(userName.NameSurname.ToString());
                userInfo["HierarchyNo"] = Server.UrlEncode(hierarchyEntity != null ? hierarchyEntity.HierarchyRank.ToString() : "NULL");
                userInfo.Expires = DateTime.Now.AddHours(2);
                HttpContext.Response.SetCookie(userInfo);

            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cookies["userInfo"].Expires = DateTime.Now.AddSeconds(1);
            return RedirectToAction("Login");
        }

    }
}
