using MiniProject.Repo;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject.Web.Controllers
{
    public class MMenuController : Controller
    {
        // GET: Menu_
        public ActionResult Index()
        {
            ViewBag.ListCode = new SelectList(MMenuRepo.get(), "code", "code");
            ViewBag.ListName = new SelectList(MMenuRepo.get(), "name", "name");
            List<MMenuVM> data = MMenuRepo.get();
            return View(data);
        }

        public ActionResult List()
        {
            List<MMenuVM> data = MMenuRepo.get();
            return PartialView("_List", data);
        }

        public ActionResult Add()
        {
            MMenuVM model = new MMenuVM();
            model.code = MMenuRepo.NewCode();
            ViewBag.ListParent = new SelectList(MMenuRepo.get(), "name", "name");
            return PartialView("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MMenuVM model)
        {
            if (ModelState.IsValid)
            {
                if (MMenuRepo.CekNama(model)==true)
                {
                    var result = new
                    {
                        success = false,
                        alertType = "error",
                        alertStrong = "Error!",
                        alertMessage = "Menu name already used"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MMenuRepo.CekNama(model)==false)
                {
                    MMenuRepo.insert(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertStrong = "Data Saved !",
                        alertMessage = "New menu has been added with code " + model.code + " !"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return PartialView(model);
        }

        public JsonResult CekNama(string nama)
        {
            var result = new
            {
                success = true,
                data = MMenuRepo.CekNama(nama),
                alertType = "warning",
                alertStrong = "Error !",
                alertMessage = "Menu name with name </strong>" + nama + "</strong> already used"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
 
        public ActionResult Edit(int id)
        {
            ViewBag.ListParent = new SelectList(MMenuRepo.get(), "name", "name");
            return PartialView("_Edit", MMenuRepo.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MMenuVM model)
        {
            if (ModelState.IsValid)
            {
                if (MMenuRepo.CekNama2(model) == true)
                {
                    var result = new
                    {
                        success = false,
                        alertType = "error",
                        alertStrong = "Error!",
                        alertMessage = "Menu name already used"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MMenuRepo.CekNama2(model) == false)
                {
                    MMenuRepo.Edit(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertStrong = "Data Updated !",
                        alertMessage = "Data menu has been updated !"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return PartialView("_Edit", model);
        }

        public ActionResult HideMenu(int id)
        {
            if (MMenuRepo.hiddenMenu(id) == true)
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
            MMenuVM menu = MMenuRepo.getById(id);
            return PartialView("_Detail", menu);
        }

    }
}