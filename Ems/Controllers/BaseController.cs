using Ems.Service.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ems.Controllers
{
    public class BaseController : Controller
    {
        private readonly DoctorManager dManager;
        public BaseController()
        {
            dManager = new DoctorManager();
        }
        internal void AddSession(string Email)
        {
            var userName = dManager.GetAllByParameter(x=>x.Mail == Email);
            if(userName.FirstOrDefault() != null)
            {
                Session["User"] = userName.FirstOrDefault();
                FormsAuthentication.SetAuthCookie(userName.FirstOrDefault().NameSurname, false);
            }
            
        }
    }
}