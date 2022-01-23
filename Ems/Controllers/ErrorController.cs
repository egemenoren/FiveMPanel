using Ems.Service.Logger;
using Ems.Service.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Controllers
{
    
    public class ErrorController : BaseController
    {
        private LogManager _logManager;
        private UserManager _userManager;
        public ErrorController()
        {
            _userManager = new UserManager();
            _logManager = LogManager.GetInstance();
        }
        public ActionResult PageNotFound()
        {
            try
            {
                var Email = Session["Email"] ?? null;
                var user = _userManager.GetByParameter(x => x.Mail == Email);
                _logManager.Error(user.Id, user.Mail, null, "PageNotFound", "PageNotFound", HttpContext.Request.Url.AbsoluteUri, Data.Model.Log.LogType.Generic);
            }
            catch(Exception ex)
            {
                var Email = Session["Email"] ?? null;
                var user = _userManager.GetByParameter(x => x.Mail == Email);
                _logManager.Error(user.Id, user.Mail, Request.Url.AbsolutePath, ex.Message, ex.StackTrace, Request.Url.AbsoluteUri, Data.Model.Log.LogType.Generic);
            }
            return View();
        }
    }
}