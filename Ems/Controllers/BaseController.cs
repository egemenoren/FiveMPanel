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
        public BaseController()
        {
        }
        internal bool CheckAccessForJobs(string JobName,bool Manager,string UserJobName)
        {
            if (Manager || JobName == UserJobName)
            {
                return true;
            }
            else
                return false;
        }
        public void AddSession(string Email)
        {
            UserManager userManager = new UserManager();
            RankManager rankManager = new RankManager();
            JobManager jobManager = new JobManager();
            var userName = userManager.GetAllByParameter(x => x.Mail == Email).FirstOrDefault();
            if (userName != null)
            {
                rankManager = new RankManager();
                jobManager = new JobManager();
                HierarchyManager hierarchyManager = new HierarchyManager();
                Session["User"] = userName;
                FormsAuthentication.SetAuthCookie(userName.NameSurname, false);
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["RankId"] = Server.UrlEncode(userName.RankId.ToString());
                userInfo["JobId"] = Server.UrlEncode(userName.JobId.ToString());
                var rankEntity = rankManager.GetAllByParameter(x => x.Id == userName.RankId).FirstOrDefault();
                var jobEntity = jobManager.GetAllByParameter(x => x.Id == userName.JobId).FirstOrDefault();
                var hierarchyEntity = hierarchyManager.GetByParameter(x => x.RankId == rankEntity.Id);
                userInfo["JobName"] = Server.UrlEncode(jobEntity.JobName);
                userInfo["RankName"] = Server.UrlEncode(rankEntity.RankName);
                userInfo["AccessJobPanel"] = Server.UrlEncode(rankEntity.AccessJobPanel.ToString());
                userInfo["AccessManagementPanel"] = Server.UrlEncode(userName.AccessManagementPanel.ToString());
                userInfo["NameSurname"] = Server.UrlEncode(userName.NameSurname.ToString());
                userInfo["HierarchyNo"] = Server.UrlEncode(hierarchyEntity.HierarchyRank.ToString());
                userInfo.Expires = DateTime.Now.AddHours(2);
                HttpContext.Response.SetCookie(userInfo);
            }
        }

    }
}