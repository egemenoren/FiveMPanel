using Ems.Data.Model;
using Ems.Service.Management;
using Ems.ViewModels;
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
        private JobManager jobManager;
        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            return View();
        }
        public ActionResult AddUser()
        {
            jobManager = new JobManager();
            rankManager = new RankManager();
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            var rankList = rankManager.GetAll();
            var jobList = jobManager.GetAll();
            addUserViewModel.Jobs = jobList;
            addUserViewModel.Ranks = rankList;
            return View(addUserViewModel);
        }
        [HttpGet]
        public JsonResult RemoveRank(int id)
        {
            rankManager = new RankManager();
            rankManager.Remove(id);
            var newRankList = rankManager.GetAll();
            return Json(newRankList, JsonRequestBehavior.AllowGet);
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
            jobManager = new JobManager();
            if (Job == null)
            {
                var rankListAll = rankManager.GetAll();
                return Json(rankListAll, JsonRequestBehavior.AllowGet);
            }
            var jobEntity = jobManager.GetAllByParameter(x => x.JobName == Job).FirstOrDefault();
            var rankList = rankManager.GetAllByParameter(x => x.JobId == jobEntity.Id);
            return Json(rankList,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddRank(string RankName, string Job, bool AccessJobPanel = false, bool AccessManagementPanel = false)
        {
            try
            {

                rankManager = new RankManager();
                jobManager = new JobManager();
                var jobEntity = jobManager.GetAllByParameter(x => x.JobName == Job).FirstOrDefault();
                if (rankManager.CheckIfExists(x => x.JobId == jobEntity.Id && x.RankName == RankName))
                {
                    ViewBag.ErrMsg = "Bu departmana ait " + RankName + " isimli rank zaten mevcut.";
                    return View();
                }
                rankManager.Add(new Data.Model.Ranks
                {
                    RankName = RankName,
                    JobId = jobEntity.Id,
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