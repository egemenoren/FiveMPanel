using Ems.Service.Logger;
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
        public HomeController()
        {
            
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        

    }
}
