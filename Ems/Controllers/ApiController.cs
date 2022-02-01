using Ems.Data.Model;
using Ems.Service.Logger;
using Ems.Service.Management;
using Ems.Service.Utility;
using Ems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ems.Controllers
{
    public class ApiController : Controller
    {
        private MenusManager menusManager;
        private UserManager userManager;
        private RankManager rankManager;
        private JobManager jobManager;
        private SubMenusManager subMenusManager;
        private MenusManager menuPermissions;
        private MainMenusManager mainMenusManager;
        public ApiController()
        {
            userManager = new UserManager();
            menusManager = new MenusManager();
            jobManager = new JobManager();
            rankManager = new RankManager();
            subMenusManager = new SubMenusManager();
            menuPermissions = new MenusManager();
            mainMenusManager = new MainMenusManager();


        }

        private void LogSuccess(string action)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            LogManager.GetInstance().Info(BaseController.userSession.Id, BaseController.userSession.Mail, ActionName + " " + action, null, null, "/api/" + action, Data.Model.Log.LogType.Generic);
        }
        private void LogError(string action, Exception ex)
        {
            string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            LogManager.GetInstance().Info(BaseController.userSession.Id, BaseController.userSession.Mail, ActionName + " cannot be" + action, ex.Message, ex.StackTrace, "/api/" + action, Data.Model.Log.LogType.Generic);

        }
        [HttpPost]
        public JsonResult EditRankPermissions(List<SubMenusViewModel> model)
        {
            JsonFramework json = new JsonFramework();
            try
            {
                foreach (var item in model)
                {
                    menusManager.EditPermissions(BaseController.userSession.Id, (int)item.RankId, item.SubMenuId, (bool)item.HasPermission);
                }
                json.Message = "Helalke";
                LogSuccess("updated");
                return Json(new { json }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError("updated ", ex);
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult RemoveRank(int id)
        {
            try
            {
                rankManager = new RankManager();
                var rankMenus = menusManager.GetAllByParameter(x => x.RankId == id);
                foreach (var item in rankMenus)
                {
                    menusManager.Remove(item.Id);
                }
                rankManager.Remove(id);
                LogSuccess("removed with id " + id);
                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError("removed with id " + id, ex);
                return Json("Hata", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult GetRanks(string Job)
        {
            try
            {
                List<RankManagementViewModel> modelList = new List<RankManagementViewModel>();
                var jobEntity = jobManager.GetAllByParameter(x => x.JobName == Job).FirstOrDefault();
                var jobList = Job != "" && Job != null ? rankManager.GetAllByParameter(x => x.JobId == jobEntity.Id) : rankManager.GetAll();
                if (Job != "" && Job != null)
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
                LogSuccess("called");
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return new HttpStatusCodeResult(500);
            }

        }
        [HttpPost]
        public ActionResult GetJobs()
        {
            try
            {
                var jobList = jobManager.GetAll();
                LogSuccess("called");
                return Json(jobList);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return new HttpStatusCodeResult(500);
            }
        }
        [HttpPost]
        public ActionResult GetUsers()
        {
            try
            {
                var userList = userManager.GetAll();
                List<UserManagementViewModel> model = new List<UserManagementViewModel>();
                foreach (var item in userList)
                {
                    var userJob = jobManager.GetByParameter(x => x.Id == item.JobId);
                    var userRank = rankManager.GetByParameter(x => x.Id == item.RankId);
                    model.Add(new UserManagementViewModel
                    {
                        NameSurname = item.NameSurname,
                        JobName = userJob.JobName,
                        RankName = userRank != null ? userRank.RankName : "Rankı Silinmiş",
                        Id = item.Id
                    });
                };
                LogSuccess("called");
                return Json(model);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return new HttpStatusCodeResult(500);
            }
        }
        [HttpPost]
        public JsonResult GetInsurances()
        {
            try
            {
                var insurances = new InsuranceManager().GetAll();
                LogSuccess("called");
                return Json(insurances);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return Json("Unsuccess");
            }

        }
        [HttpPost]
        public JsonResult RemoveInsurance(int id)
        {
            try
            {
                new InsuranceManager().Remove(id);
                LogSuccess("removed id " + id);
                return Json("Success");
            }
            catch (Exception ex)
            {
                LogError("removed", ex);
                return Json("Unsuccess");
            }
        }
        [HttpPost]
        public JsonResult GetMainMenus()
        {
            try
            {
                var menus = mainMenusManager.GetAll();
                LogSuccess("called");
                return Json(menus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return Json("Unsuccess");
            }

        }
        [HttpPost]
        public JsonResult GetProcesses()
        {
            try
            {
                var entity = new ProcessManager().GetAll();
                LogSuccess("called");
                return Json(entity);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return Json("Unsuccess");
            }

        }
        [HttpPost]
        public JsonResult RemoveProcess(int id)
        {
            try
            {
                new ProcessManager().Remove(id);
                LogSuccess("removed id " + id);
                return Json("Success");
            }
            catch (Exception ex)
            {
                LogError("removed id " + id, ex);
                return Json("Unsuccess");
            }


        }
        [HttpPost]
        public JsonResult GetDoctors()
        {
            try
            {
                List<UserManagementViewModel> model = new List<UserManagementViewModel>();
                var doctorList = userManager.GetAllByParameter(x => x.JobId == 2);
                foreach (var item in doctorList)
                {
                    var rank = rankManager.GetById(item.RankId);
                    model.Add(new UserManagementViewModel
                    {
                        NameSurname = item.NameSurname,
                        RankName = rank.RankName,
                        Id = item.Id
                    });
                }
                LogSuccess("called");
                return Json(model);
            }
            catch (Exception ex)
            {
                LogError("called", ex);
                return Json("Unsuccess " + ex.Message);
            }
        }
        [HttpPost]
        public JsonResult FireDoctor(int id)
        {
            try
            {
                JsonFramework json = new JsonFramework();
                var currentUser = BaseController.userSession;
                var userToFire = userManager.GetById(id);
                var currentUserRank = rankManager.GetById(currentUser.RankId);
                var userToFireRank = rankManager.GetById(userToFire.RankId);
                if (currentUser.AccessManagementPanel == true && userToFire.AccessManagementPanel == false)
                {
                    json.Message = "Başarıyla kovuldu";
                    json.Status = "success";
                }
                else if (currentUser.AccessManagementPanel == true && userToFire.AccessManagementPanel == true)
                {
                    json.Message = "Aynı yetkide olduğun birini kovamazsın aslan parçası";
                    json.Status = "error";
                }
                else if (currentUser.AccessManagementPanel == false && userToFire.AccessManagementPanel == true)
                {
                    json.Message = "Kendinden büyüklerle uğraşma evladım öyle bi yetkin yok maalesef";
                    json.Status = "error";
                }
                else if ((currentUser.AccessManagementPanel == false && userToFire.AccessManagementPanel == false) && (userToFireRank.AccessJobPanel == true && currentUserRank.AccessJobPanel == true))
                {
                    json.Message = "Aynı yetkide olduğun birini kovamazsın aslan parçası";
                    json.Status = "error";
                }
                else
                {
                    json.Message = "Başarıyla kovuldu";
                    json.Status = "success";
                }
                LogSuccess("with id " + id);
                return Json(new { json });
            }
            catch (Exception ex)
            {
                LogError("fired with id " + id, ex);
                JsonFramework json = new JsonFramework();
                json.Status = "error";
                json.Message = ex.Message;
                return Json(new { json });
            }
        }
        [HttpPost]
        public JsonResult CheckIfHasInsurance(string NameSurname)
        {
            JsonFramework json = new JsonFramework();
            try
            {
                RegisterInsuranceManager registerInsuranceManager = new RegisterInsuranceManager();
                var insurance = registerInsuranceManager.GetByParameter(x => x.NameSurname == NameSurname && x.ExpireDate >= DateTime.Now);
                if (insurance != null && insurance.CreditsLeft > 0)
                {
                    json.Status = "Success";
                    json.Message = insurance.CreditsLeft + " Kredisi var.";
                    json.Data = insurance;
                    return Json(new { json });
                }
                else if (insurance != null && insurance.CreditsLeft <= 0)
                {
                    json.Status = "Unsuccess";
                    json.Message = "Kredisi tükenmiş";
                }
                else
                {
                    json.Status = "Unsuccess";
                    json.Message = "Sigortası Yok";
                }
                LogSuccess("checked");
                return Json(new { json });
            }
            catch (Exception ex)
            {
                json.Status = "Unsuccess";
                json.Message = "Hata Oluştu";
                LogError("checked", ex);
                return Json(new { json });
            }

        }
        [HttpPost]
        public JsonResult GetInsuranceById(int id)
        {
            JsonFramework json = new JsonFramework();
            try
            {
                InsuranceManager insuranceManager = new InsuranceManager();
                var entity = insuranceManager.GetById(id);
                json.Data = entity;
                json.Status = "Success";
                json.Message = "Başarıyla Çekildi";
                return Json(new { json });
            }
            catch (Exception ex)
            {
                json.Status = "Unsuccess";
                json.Message = ex.Message;
                return Json(new { json });
            }


        }
        [HttpPost]
        public JsonResult GetExaminationList()
        {
            JsonFramework json = new JsonFramework();
            try
            {
                ExaminationManager examinationManager = new ExaminationManager();
                UserManager userManager = new UserManager();
                List<AddExaminationViewModel> list = new List<AddExaminationViewModel>();
                var examinations = examinationManager.GetAll();
                foreach (var item in examinations)
                {
                    list.Add(new AddExaminationViewModel
                    {
                        DoctorName = userManager.GetById(item.DoctorId).NameSurname,
                        Diagnosis = item.Diagnosis,
                        NameSurname = item.NameSurname,
                        Price = item.Price,
                        IsJudical = item.isJudical

                    });
                }
                json.Data = list;
                json.Message = "Success";
                return Json(list);
            }
            catch
            {
                json.Message = "Unsuccess";
                return Json(new { json });
            }
        }
    }
}