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
        private HierarchyManager hierarchyManager;
        public ManagementController()
        {
            userManager = new UserManager();
            jobManager = new JobManager();
            rankManager = new RankManager();
            hierarchyManager = new HierarchyManager();
        }
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
                ViewBag.Success = "Kullanıcı başarıyla eklendi";

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
            List<UserManagementViewModel> listModel = new List<UserManagementViewModel>();
            try
            {
                rankManager = new RankManager();
                jobManager = new JobManager();
                userManager = new UserManager();
                var userList = userManager.GetAll();

                foreach (var item in userList)
                {
                    var rankEntity = rankManager.GetAllByParameter(x => x.Id == item.RankId).FirstOrDefault();
                    var jobEntity = jobManager.GetAllByParameter(x => x.Id == rankEntity.JobId).FirstOrDefault();
                    UserManagementViewModel model = new UserManagementViewModel
                    {
                        Id = item.Id,
                        NameSurname = item.NameSurname,
                        AccessManagement = item.AccessManagementPanel,
                        JobId = jobEntity.Id,
                        RankName = rankEntity.RankName,
                        RankId = rankEntity.Id,
                        JobName = jobEntity.JobName
                    };
                    listModel.Add(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View(listModel);


        }
        public ActionResult AddRank()
        {
           var model = jobManager.GetAll();
            return View(model);
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
                    var rankId = rankManager.GetById(item.Id).Id;
                    var jobName = jobManager.GetAllByParameter(x => x.Id == item.JobId).FirstOrDefault().JobName;
                    modelList.Add(new RankManagementViewModel
                    {
                        Id = rankId,
                        JobName = jobName,
                        AccessJobPanel = item.AccessJobPanel,
                        RankName = item.RankName
                    });
                }
            }

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddRank(byte HierarchyNo, string RankName, string Job, bool AccessJobPanel = false)
        {
            try
            {

                rankManager = new RankManager();
                jobManager = new JobManager();
                var jobEntity = jobManager.GetAllByParameter(x => x.JobName == Job).FirstOrDefault();
                var rankEntity = rankManager.GetByParameter(x => x.JobId == jobEntity.Id && x.RankName == RankName);
                if (rankEntity != null)
                {
                    ViewBag.ErrMsg = "Bu departmana ait " + RankName + " isimli rank zaten mevcut.";
                    return View(jobManager.GetAll());
                }
                rankManager.Add(new Ranks
                {
                    RankName = RankName,
                    JobId = jobEntity.Id,
                    AccessJobPanel = AccessJobPanel,
                    HierarchyNo = HierarchyNo
                });
                ViewBag.Success = "Rank Başarıyla Eklendi!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View(jobManager.GetAll());
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
            List<UserManagementViewModel> model = new List<UserManagementViewModel>();
            try
            {
                userManager = new UserManager();
                jobManager = new JobManager();
                rankManager = new RankManager();

                var jobEntity = jobManager.GetAllByParameter(x => x.JobName == JobName).FirstOrDefault();
                var userList = jobEntity != null ? userManager.GetAllByParameter(x => x.JobId == jobEntity.Id) : userManager.GetAll();

                foreach (var item in userList)
                {
                    var rankEntity = rankManager.GetAllByParameter(x => x.Id == item.RankId).FirstOrDefault();
                    jobEntity = jobManager.GetAllByParameter(x => x.Id == rankEntity.JobId).FirstOrDefault();
                    model.Add(new UserManagementViewModel
                    {
                        Id = item.Id,
                        JobId = jobEntity.Id,
                        JobName = jobEntity.JobName,
                        RankId = rankEntity.Id,
                        RankName = rankEntity.RankName,
                        AccessManagement = item.AccessManagementPanel,
                        NameSurname = item.NameSurname
                    });
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpGet]
        public JsonResult RemoveRank(int id)
        {
            rankManager = new RankManager();
            rankManager.Remove(id);
            return Json("Başarılı", JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditRank(int Id)
        {
            var rankEntitiy = rankManager.GetById(Id);
            var jobEntity = jobManager.GetById(rankEntitiy.JobId);
            var hierarchyNo = hierarchyManager.GetByParameter(x => x.RankId == rankEntitiy.Id).HierarchyRank;
            RankManagementViewModel model = new RankManagementViewModel
            {
                Id = rankEntitiy.Id,
                RankName = rankEntitiy.RankName,
                AccessJobPanel = rankEntitiy.AccessJobPanel,
                JobName = jobEntity.JobName,
                HierarchyNo = hierarchyNo

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditRank(int RankId, string RankName, short hierarchyNo, string Job, bool AccessJobPanel = false)
        {
            try
            {
                var rankEntity = rankManager.GetByParameter(x => x.Id == RankId);
                var jobEntity = jobManager.GetByParameter(x => x.JobName == Job);
                var hierarchyEntity = hierarchyManager.GetByParameter(x => x.RankId == rankEntity.Id);
                if (hierarchyManager.GetByParameter(x => x.JobId == jobEntity.Id && x.HierarchyRank == hierarchyNo) != null)
                    throw new Exception(jobEntity.JobName + " Mesleğinde aynı hiyerarşi numarasına sahip bir rank zaten var.");
                rankEntity.RankName = RankName;
                rankEntity.AccessJobPanel = AccessJobPanel;
                rankEntity.JobId = jobEntity.Id;
                rankManager.Update(rankEntity);
                hierarchyEntity.RankId = rankEntity.Id;
                hierarchyEntity.HierarchyRank = hierarchyNo;
                hierarchyEntity.JobId = jobEntity.Id;
                hierarchyManager.Update(hierarchyEntity);
                ViewBag.Success = "Rank Başarıyla Düzenlendi";
                TempData["Success"] = "Rank Başarıyla Düzenlendi";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                TempData["ErrMsg"] = ex.Message;
            }

            return RedirectToAction("RankManagement");
        }

        public ActionResult EditUser(int Id)
        {
            var userEntity = userManager.GetById(Id);
            var jobList = jobManager.GetAll();
            var rankName = rankManager.GetById(userEntity.RankId).RankName;
            AddUserViewModel model = new AddUserViewModel
            {
                AccessManagementPanel = userEntity.AccessManagementPanel,
                DiscordId = (ulong)userEntity.DiscordId,
                Email = userEntity.Mail,
                JobId = userEntity.JobId,
                RankId = userEntity.RankId,
                Password = userEntity.Password,
                SteamHex = userEntity.HexId,
                Jobs = jobList,
                NameSurname = userEntity.NameSurname,
                RankName = rankName,
                Id=Id

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditUser(int UserId, long DiscordId,
            int JobId,int RankId,
            string HexId,string NameSurname, bool AccessManagementPanel = false)
        {
            try
            {
                var entityToUpdate = userManager.GetById(UserId);
                entityToUpdate.AccessManagementPanel = AccessManagementPanel;
                entityToUpdate.DiscordId = DiscordId;
                entityToUpdate.HexId = HexId;
                entityToUpdate.JobId = JobId;
                entityToUpdate.NameSurname = NameSurname;
                entityToUpdate.RankId = RankId;
                userManager.Update(entityToUpdate);
                ViewBag.Success = "Rank Başarıyla Düzenlendi";
                TempData["Success"] = "Rank Başarıyla Düzenlendi";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                TempData["ErrMsg"] = ex.Message;
            }

            return RedirectToAction("UserManagement");
        }
    }
}