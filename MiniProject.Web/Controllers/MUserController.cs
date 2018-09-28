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
    public class MUserController : Controller
    {
        // GET: MUser
        public ActionResult Index()
        {
            ViewBag.Title = "List User";
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "name", "name");
            ViewBag.ListEmployee = new SelectList(MEmployeeRepo.get(), "FullName", "FullName");
            ViewBag.ListCompany = new SelectList(MCompanyRepo.get(), "name", "name");
            return View(MUserRepo.get());
        }
        public ActionResult List()
        {
            return PartialView("_List", MUserRepo.get());
        }
        public ActionResult Details(int id)
        {
            var data = MUserRepo.getById(id);
            ViewBag.Title = "View User - " + data.firtsName + " " + data.lastName + "(" + data.username + ")";
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            ViewBag.ListEmployee = new SelectList(MEmployeeRepo.get(), "id", "fullName");
            return PartialView("_Details", data);
        }
        public ActionResult Add()
        {
            ViewBag.Title = "Add User";
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            ViewBag.ListEmployee = new SelectList(MEmployeeRepo.getDataNonUser(), "id", "FullName");
            return PartialView("_Add");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MUserVM model)
        {
            if (ModelState.IsValid && MUserRepo.insert(model))
            {
                var result = new
                {
                    success = true,
                    message = "Data Saved! New User has been add with username " + model.username,
                    vclass = "alert alert-info"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Title = "Add User";
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            ViewBag.ListEmployee = new SelectList(MEmployeeRepo.getDataNonUser(), "id", "FullName");
            return PartialView("_Add", model);
        }

        public ActionResult Edit(int id)
        {
            var data = MUserRepo.getById(id);
            ViewBag.Title = "Edit User - " + data.firtsName + " " + data.lastName + "(" + data.username + ")";
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            ViewBag.ListEmployee = new SelectList(MEmployeeRepo.getDataNonUser(), "id", "FullName");
            return PartialView("_Edit", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MUserVM model)
        {
            var data = MUserRepo.getById(model.id);
            if (ModelState.IsValid && MUserRepo.update(model))
            {
                var result = new
                {
                    success = true,
                    message = "Data Updated! Data User has been updated !",
                    vclass = "alert alert-info"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Title = "Edit User - " + data.firtsName + " " + data.lastName + "(" + data.username + ")";
            ViewBag.ListRole = new SelectList(MRoleRepo.get(), "id", "name");
            ViewBag.ListEmployee = new SelectList(MEmployeeRepo.getDataNonUser(), "id", "FullName");
            return PartialView("_Edit", model);
        }

        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Data?";
            var data = MUserRepo.getById(id);
            return PartialView("_Delete", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MUserVM model)
        {
            if (MUserRepo.delete(model.id))
            {
                var result = new
                {
                    success = true,
                    message = "Data Deleted! Data User with Username" + model.username + " has been deleted!",
                    vclass = "alert alert-info"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_Delete", model);
        }

        public ActionResult Search(string user)
        {
            var data = MUserRepo.search(user);
            return PartialView("_List", data);
        }
    }
}