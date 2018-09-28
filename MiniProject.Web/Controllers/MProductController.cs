using MiniProject.Repo;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject.Web.Controllers
{
    
    public class MProductController : Controller
    {
        // GET: MProduct
        public ActionResult Index()
        {
            var data = MProductRepo.GetAllData();
            return View(data);
        }

        public ActionResult List()
        {
            var data = MProductRepo.GetAllData();
            return PartialView("_List", data);
        }

        public ActionResult Detail(int id)
        {
            var jsonParse = new
            {
                data = MProductRepo.GetId(id)
            };
            Json(jsonParse, JsonRequestBehavior.AllowGet);

            return PartialView("_Detail", jsonParse.data);
        }

        public ActionResult Insert()
        {
            ViewBag.AutoCode = MProductRepo.AutoCode();
            return PartialView("_Insert");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(MProductVM model)
        {
            if (ModelState.IsValid)
            {
                if (MProductRepo.CheckIfExists(model) == true)
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
                    var user = User.Identity.Name;
                    MProductRepo.Insert(model, user);
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
            return PartialView("_Update", MProductRepo.GetId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MProductVM model)
        {
            if (ModelState.IsValid)
            {
                if (MProductRepo.CheckIfExists(model) == true)
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
                    MProductRepo.Update(model);
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
            var data = MProductRepo.GetId(id);
            MProductRepo.Delete(id);
            var result = new
            {
                success = true,
                item = data,
                alertType = "info",
                alertMessage = "Deleted !",
                alertStrong = "Your Data With Code <strong>(" + data.code + ")</strong> is Deleted"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}