using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniProject.ViewModel;
using MiniProject.Repo;

namespace MiniProject.Web.Controllers
{
    [Authorize]
    public class MSouvenirController : Controller
    {
        public ActionResult Masking()
        {
            return View();
        }

        // GET: MSouvenir
        public ActionResult Index()
        {
            var data = MSouvenirRepo.GetAllData();

            ViewBag.listUnit = new SelectList(mUnitRepo.get(), "name", "name");
            return View(data);
        }

        public ActionResult List()
        {
            var data = MSouvenirRepo.GetAllData();
            return PartialView("_List", data);
        }

        public ActionResult Detail(int id)
        {
            var jsonParse = new
            {
                data = MSouvenirRepo.GetId(id)
            };
            Json(jsonParse, JsonRequestBehavior.AllowGet);

            ViewBag.listUnit = new SelectList(mUnitRepo.get(), "id", "name");
            return PartialView("_Detail", jsonParse.data);
        }

        public ActionResult SearchData(MSouvenirVM model)
        {
            var data = MSouvenirRepo.SearchData(model);
            if (data.Count == 0 && model.code == null && model.name == null && model.m_unit_id == 0 && model.created_date == DateTime.MinValue && model.created_by == 0)
            {
                data = MSouvenirRepo.GetAllData();
            }

            return PartialView("_List", data);
        }

        public ActionResult Insert()
        {
            ViewBag.AutoCode = MSouvenirRepo.AutoCode();
            ViewBag.listUnit = new SelectList(mUnitRepo.get(), "id", "name");
            return PartialView("_Insert");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(MSouvenirVM model)
        {
            if (ModelState.IsValid)
            {
                if (MSouvenirRepo.CheckIfExists(model) == true)
                {
                    var notif = new
                    {
                        success = false,
                        alertType = "error",
                        alertMessage = "ERROR !",
                        alertStrong = "Your Data With Name (" + model.name + ") is Already Exists"
                    };
                    return Json(notif, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    MSouvenirRepo.Insert(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertMessage = "Saved !",
                        alertStrong = "Your Data With Code <strong>(" + model.code + ")</strong> is Saved"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_Insert");
        }

        public ActionResult Update(int id)
        {
            ViewBag.listUnit = new SelectList(mUnitRepo.get(), "id", "name");
            return PartialView("_Update", MSouvenirRepo.GetId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MSouvenirVM model)
        {
            if (ModelState.IsValid)
            {
                if (MSouvenirRepo.CheckIfExists(model) == true)
                {
                    var notif = new
                    {
                        success = false,
                        alertType = "error",
                        alertMessage = "ERROR !",
                        alertStrong = "Your Data With Name <strong>(" + model.name + ")</strong> is Already Exists"
                    };
                    return Json(notif, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    MSouvenirRepo.Update(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertMessage = "Updated !",
                        alertStrong = "Your Data With Code <strong>(" + model.code + ")</strong> is Updated"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_Update");
        }

        public ActionResult Delete(int id)
        {
            var data = MSouvenirRepo.GetId(id);
            MSouvenirRepo.Delete(id);
            var result = new
            {
                success = true,
                item = data,
                alertType = "info",
                alertStrong = "Deleted !",
                alertMessage = "Your Data With Code <strong>(" + data.code + ")</strong> is Deleted"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}