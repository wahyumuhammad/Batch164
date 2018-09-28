using MiniProject.Repo;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject.Web.Controllers
{
    public class MRoleController : Controller
    {
        // GET: Role_
        public ActionResult Index()
        {
            ViewBag.ListCode = new SelectList(MRoleRepo.get(),"code","code");
            ViewBag.ListName = new SelectList(MRoleRepo.get(), "name", "name");
            List<MRoleVM> data = MRoleRepo.get();
            return View(data);
        }

        public ActionResult List()
        {
            List<MRoleVM> data = MRoleRepo.get();
            return PartialView("_List", data);
        }

        public ActionResult Add()
        {
            MRoleVM model = new MRoleVM();
            model.code = MRoleRepo.NewCode();
            return PartialView("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (MRoleRepo.CekNama(model) == true)
                {
                    var result = new
                    {
                        success = false,
                        alertType = "error",
                        alertStrong = "Error!",
                        alertMessage = "Role name already used"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MRoleRepo.CekNama(model) == false)
                {
                    MRoleRepo.insert(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertStrong = "Data Saved !",
                        alertMessage = "New role has been added with code " + model.code + " !"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_Add", model);
        }

        public JsonResult CekNama(string nama)
        {
            var result = new
            {
                success = true,
                data = MRoleRepo.CekNama(nama),
                alertType = "warning",
                alertStrong = "Error !",
                alertMessage = "Role name with name </strong>" + nama + "</strong> already used"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", MRoleRepo.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (MRoleRepo.CekNama2(model) == true)
                {
                    var result = new
                    {
                        success = false,
                        alertType = "error",
                        alertStrong = "Error!",
                        alertMessage = "Role name already used"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MRoleRepo.CekNama2(model) == false)
                {
                    MRoleRepo.Edit(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertStrong = "Data Updated !",
                        alertMessage = "Data Role has been updated !"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_Edit", model);
        }

        public ActionResult HideRole(int id)
        {
            if (MRoleRepo.hiddenRole(id) == true)
            {
                var result = new
                {
                    success = true,
                    message = "Data Berhasil Di Delete"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("index");
        }

        public ActionResult Detail(int id)
        {
            MRoleVM role = MRoleRepo.getById(id);
            return PartialView("_Detail", role);
        }
    }
}