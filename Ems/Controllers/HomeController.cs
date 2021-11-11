using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Ems.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
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
            AddSession(Email);
            if(Session["User"] != null)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Index","Home");
        }
    }
}