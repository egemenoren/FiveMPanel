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
    public class EmsManagementController : BaseController
    {
        public EmsManagementController()
        {

        }
        public ActionResult UpdatePermissions(int id)
        {
            SubMenusManager subMenusManager = new SubMenusManager();
            MenusManager menusManager = new MenusManager();
            MainMenusManager mainMenusManager = new MainMenusManager();

            var submenus = subMenusManager.GetAll();
            var menuPermissions = menusManager.GetAllByParameter(x => x.RankId == id);
            List<SubMenusViewModel> subMenusViewModels = new List<SubMenusViewModel>();
            List<SubMenus> subMenusModel = new List<SubMenus>();
            foreach (var item in submenus)
            {
                var hasPermission = menusManager.GetByParameter(x => x.SubMenuId == item.Id && x.RankId == id) != null;
                var itemMainMenu = mainMenusManager.GetByParameter(x => x.Id == item.MainMenuId);
                if (!menusManager.GetAllByParameter(x => x.SubMenuId == item.Id).Where(x=>x.RankId == userSession.RankId).Any())
                    continue;
                subMenusViewModels.Add(new SubMenusViewModel
                {
                    Action = item.Action,
                    Controller = item.Controller,
                    HasPermission = hasPermission,
                    SubMenuId = item.Id,
                    SubMenuName = item.Name,
                    MainMenuId = itemMainMenu.Id,
                    MainMenuName = itemMainMenu.MenuName,
                });
                var entity = subMenusManager.GetByParameter(x=>x.Id == item.Id);
                subMenusModel.Add(entity);
            }
            ViewBag.Menus = subMenusViewModels;
            ViewBag.SubMenus = subMenusModel;
            return View();
        }
        public ActionResult RankManagement()
        {
            return View();
        }
        public ActionResult Bills()
        {
            return View();
        }

        public ActionResult Insurances()
        {
            return View();
        }
        public ActionResult AddInsurance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInsurance(Insurance model)
        {
            try
            {
                InsuranceManager insuranceManager = new InsuranceManager();
                insuranceManager.Add(model);
                ViewBag.Success = model.PackageName + " Sigorta Paketi Başarıyla Eklendi";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
        public ActionResult UpdateInsurance(int id)
        {
            try
            {
                var entity = new InsuranceManager().GetById(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = "Bir hata oluştu";
                return View();
            }

        }
        [HttpPost]
        public ActionResult UpdateInsurance(Insurance model)
        {
            try
            {
                new InsuranceManager().Update(model);
                ViewBag.Success = "Sigorta başarıyla düzenlendi";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }

            return View();
        }
        public ActionResult Processes()
        {
            return View();
        }
        public ActionResult AddProcess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProcess(Processes model)
        {
            try
            {
                ProcessManager processManager = new ProcessManager();
                processManager.Add(model);
                ViewBag.Success = "Tedavi Başarıyla Eklendi";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
        public ActionResult UpdateProcess(int id)
        {
            try
            {
                var entity = new ProcessManager().GetById(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                return View();
            }

        }
        [HttpPost]
        public ActionResult UpdateProcess(Processes model)
        {
            try
            {
                new ProcessManager().Update(model);
                ViewBag.Success = "Başarıyla Tedavi Güncellendi";
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
            }
            return View();
        }
        public ActionResult DoctorList()
        {
            return View();
        }
    }


}