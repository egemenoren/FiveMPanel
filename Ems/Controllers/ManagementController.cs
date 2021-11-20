using Ems.Data.Model;
using Ems.Service.Management;
using Ems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ems.Controllers
{
    public class ManagementController : BaseController
    {
        // GET: Management
        private RankManager rankManager;
        private JobManager jobManager;
        private UserManager userManager;
        [HttpPost]
        public ActionResult AddUser(string Email, string NameSurname, string Password, long DiscordId, string SteamHex, int JobId, int RankId, bool AccessManagementPanel = false)
        {
            userManager = new UserManager();
            try
            {
                userManager.Add(new Users
                {
                    AccessManagementPanel = AccessManagementPanel,
                    Mail = Email,
                    HexId = SteamHex,
                    Password = Password,
                    DiscordId = DiscordId,
                    RankId = RankId,
                    NameSurname = NameSurname,
                    JobId = JobId
                });


            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            jobManager = new JobManager();
            var item = jobManager.GetAll();
            AddUserViewModel model = new AddUserViewModel { Jobs = item };
            return View(model);

        }
        public ActionResult AddUser()
        {
            jobManager = new JobManager();
            var item = jobManager.GetAll();
            AddUserViewModel model = new AddUserViewModel { Jobs = item };
            return View(model);
        }
        public ActionResult RankManagement()
        {
            rankManager = new RankManager();
            var listRanks = rankManager.GetAll();
            jobManager = new JobManager();
            List<RankManagementViewModel> model = new List<RankManagementViewModel>();
            foreach (var item in listRanks)
            {
                var jobEntity = jobManager.GetAllByParameter(x => x.Id == item.JobId).FirstOrDefault();
                model.Add(new RankManagementViewModel
                {
                    Id = item.Id,
                    RankName = item.RankName,
                    JobName = jobEntity.JobName,
                    AccessJobPanel = item.AccessJobPanel
                });

            }
            return View(model);
        }
        public ActionResult UserManagement()
        {
            rankManager = new RankManager();
            jobManager = new JobManager();
            userManager = new UserManager();
            var userList = userManager.GetAll();
            List<UserManagementViewModel> listModel = new List<UserManagementViewModel>();
            foreach (var item in userList)
            {
                var rankEntity = rankManager.GetAllByParameter(x => x.Id == item.RankId).FirstOrDefault();
                var jobEntity = jobManager.GetAllByParameter(x => x.Id == rankEntity.JobId).FirstOrDefault();
                UserManagementViewModel model = new UserManagementViewModel
                {
                    Id = item.Id,
                    NameSurname = item.NameSurname,
                    AccessJobPanel = rankEntity.AccessJobPanel,
                    JobId = jobEntity.Id,
                    RankName = rankEntity.RankName,
                    RankId = rankEntity.Id,
                    JobName = jobEntity.JobName
                };
                listModel.Add(model);
            }
            return View(listModel);

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
            List<RankManagementViewModel> modelList = new List<RankManagementViewModel>();
            var jobEntity = jobManager.GetAllByParameter(x => x.JobName == Job).FirstOrDefault();
            var jobList = Job != "" ? rankManager.GetAllByParameter(x => x.JobId == jobEntity.Id) : rankManager.GetAll();
            if (Job != "")
            {
                foreach (var item in jobList)
                {
                    var rankId = rankManager.GetById(item.Id).Id;
                    modelList.Add(new RankManagementViewModel
                    {
                        Id = rankId,
                        JobName = Job,
                        AccessJobPanel = item.AccessJobPanel,
                        RankName = item.RankName
                    });
                }
            }
            else
            {
                foreach (var item in jobList)
                {
                    var jobName = jobManager.GetAllByParameter(x => x.Id == item.JobId).FirstOrDefault().JobName;
                    modelList.Add(new RankManagementViewModel
                    {
                        JobName = jobName,
                        AccessJobPanel = item.AccessJobPanel,
                        RankName = item.RankName
                    });
                }
            }

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddRank(string HierarchyNo, string RankName, string Job, bool AccessJobPanel = false)
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
                rankManager.Add(new Ranks
                {
                    RankName = RankName,
                    JobId = jobEntity.Id,
                    AccessJobPanel = AccessJobPanel,
                });
                ViewBag.Success = "Rank Başarıyla Eklendi!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
        public JsonResult RemoveUser(int Id)
        {
            userManager = new UserManager();
            try
            {
                userManager.Remove(Id);
                return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetUsersByJob(string JobName)
        {
            userManager = new UserManager();
            jobManager = new JobManager();
            rankManager = new RankManager();

            var jobEntity = jobManager.GetAllByParameter(x => x.JobName == JobName).FirstOrDefault();
            var userList = userManager.GetAllByParameter(x => x.JobId == jobEntity.Id);
            List<UserManagementViewModel> model = new List<UserManagementViewModel>();
            foreach (var item in userList)
            {
                var rankEntity = rankManager.GetAllByParameter(x => x.Id == item.RankId).FirstOrDefault();
                model.Add(new UserManagementViewModel
                {
                    Id = item.Id,
                    JobId = jobEntity.Id,
                    JobName = jobEntity.JobName,
                    RankId = rankEntity.Id,
                    RankName = rankEntity.RankName,
                    AccessJobPanel = rankEntity.AccessJobPanel,
                    NameSurname = item.NameSurname
                });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult RemoveRank(int id)
        {
            rankManager = new RankManager();
            rankManager.Remove(id);
            return Json("Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}