using Ems.Data.Model;
using Ems.Service.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Controllers
{
    public class ManagementController : BaseController
    {
        // GET: Management
        private RankManager rankManager;
        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult RankManagement()
        {
            rankManager = new RankManager();
            var listRanks = rankManager.GetAll();
            return View(listRanks);
        }
        public ActionResult AddRank()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetRanks(string Job)
        {
            rankManager = new RankManager();
            var jobList = Job != "" ? rankManager.GetAllByParameter(x => x.Job == Job) : rankManager.GetAll();
            return Json(jobList,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddRank(string RankName, string Job, bool AccessJobPanel = false, bool AccessManagementPanel = false)
        {
            try
            {

                rankManager = new RankManager();
                if (rankManager.CheckIfExists(x => x.Job == Job && x.RankName == RankName))
                {
                    ViewBag.ErrMsg = "Bu departmana ait " + RankName + " isimli rank zaten mevcut.";
                    return View();
                }
                rankManager.Add(new Data.Model.Ranks
                {
                    RankName = RankName,
                    Job = Job,
                    AccessJobPanel = AccessJobPanel,
                    AccessManagementPanel = AccessManagementPanel
                });
                ViewBag.Success = "Rank Başarıyla Eklendi!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
    }
}