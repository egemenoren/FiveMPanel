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
    [Authorize]
    public class EmsController : BaseController
    {
        // GET: Ems
        public ActionResult AddExamination()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExamination(AddExaminationViewModel model)
        {
            try
            {
                RegisterInsuranceManager registerInsurance = new RegisterInsuranceManager();
                var hasInsurance = registerInsurance.CheckHasValidInsurance(model.NameSurname);
                if (hasInsurance != null)
                    registerInsurance.UseCredits(hasInsurance);
                ExaminationManager examinationManager = new ExaminationManager();
                examinationManager.Add(new Examination
                {
                    HaveInsurance = hasInsurance != null,
                    Price = model.Price,
                    ProcessId = model.ProcessId,
                    Diagnosis = model.Diagnosis + " + " + model.Extras,
                    DoctorId = userSession.Id,
                    InsuranceId = hasInsurance != null ? hasInsurance.Id : -1,
                    isJudical = model.IsJudical,
                    NameSurname = model.NameSurname,
                    OfficerName = model.OfficerNameSurname,
                    CreatedBy = userSession.Id
                });
                ViewBag.Success = "Hastanın kaydını yapmışaz ağam O7";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
        public ActionResult RegisterInsurance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterInsurance(RegisterInsurance model)
        {
            try
            {
                RegisterInsuranceManager registerInsuranceManager = new RegisterInsuranceManager();
                InsuranceManager insuranceManager = new InsuranceManager();
                var insurancePackage = insuranceManager.GetById(model.InsuranceId);
                model.CreditsLeft = (short)insurancePackage.Credits;
                model.DoctorId = BaseController.userSession.Id;
                registerInsuranceManager.Add(model);
                ViewBag.Success = "Sağlık sigortası başarıyla yapıldı";
            }
            catch(Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
    }
}