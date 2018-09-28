using MiniProject.Repo;
using MiniProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject.Web.Controllers
{
    [Authorize]
    public class MMenuAccessController : Controller
    {
        // GET: MMenuAccess
        public ActionResult Index()
        {
            ViewBag.ListCode = new SelectList(MRoleRepo.get(), "code", "code");
            ViewBag.ListName = new SelectList(MRoleRepo.get(), "name", "name");
            ViewBag.Title = "List Menu Access";
            return View(MRoleRepo.get());
        }
        public ActionResult List()
        {
            return PartialView("_List", MRoleRepo.get());
        }

        public ActionResult Add()
        {
            ViewBag.Title = "Add List Menu Access";
            ViewBag.ListMenu = new SelectList(MMenuRepo.get(), "id", "name");
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            return PartialView("_Add");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MMenuAccessVM model)
        {
            if (ModelState.IsValid && MRoleRepo.insert(model))
            {
                var result = new
                {
                    success = true,
                    message = "Data Saved! New menu access for role " + model.role.name + " has been added !",
                    vClass = "alert alert-info"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Title = "Add List Menu Access";
            ViewBag.ListMenu = new SelectList(MMenuRepo.get(), "id", "name");
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            return PartialView("_Add", model);
        }

        public ActionResult Details(int idRole)
        {
            var data = MRoleRepo.getByIdRole(idRole);
            ViewBag.Title = "View Menu Access - "+data.role.name+" ("+data.role.code+")";
            return PartialView("_Details",data);
        }

        public ActionResult Edit(int idRole)
        {
            var data = MRoleRepo.getByIdRole(idRole);
            
            ViewBag.Title = "Update Menu Access - "+data.role.name+" ("+data.role.code+")";
            ViewBag.ListMenu = new SelectList(MMenuRepo.get(), "id", "name");
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "code");
            return PartialView("_Edit", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MMenuAccessVM model)
        {
            var data = MRoleRepo.getByIdRole(model.role.id);
            var user = User.Identity.Name;
            if (ModelState.IsValid && MRoleRepo.update(model))
            {
                var res = new
                {
                    success = true,
                    message = "Data Updated! Menu access for "+data.role.name+" has been updated !",
                    vClass = "alert alert-info"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            
            ViewBag.Title = "Update Menu Access - "+data.role.name+" ("+data.role.code+")";
            ViewBag.ListMenu = new SelectList(MMenuRepo.get(), "id", "name");
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            return PartialView("_Edit");
        }
    }
}