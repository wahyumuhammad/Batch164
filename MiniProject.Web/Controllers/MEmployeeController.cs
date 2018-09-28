using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniProject.Model;
using MiniProject.VM;
using MiniProject.Repo;
namespace MiniProject.Web.Controllers
{
   
    public class MEmployeeController : Controller
    {
        // GET: m_employee
        public ActionResult Index()
        {
            ViewBag.Title = "List Employee";
            
            ViewBag.ListCompanyName = new SelectList(MEmployeeRepo.get(), "NmCompany", "NmCompany");

            List<MEmployeeVM> item = MEmployeeRepo.get();
            return View(item);
        }

        public ActionResult List()
        {
            ViewBag.Title = "List Employee";
            List<MEmployeeVM> item = MEmployeeRepo.get();
            return PartialView("_List",item);
        }

        public ActionResult Add()
        {
            MEmployeeVM model = new MEmployeeVM();
            ViewBag.ListCompany = new SelectList(MCompanyRepo.get(), "id", "name");
            ViewBag.Title = "Add";
            return PartialView("_Add",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MEmployeeVM model)
        {
            ViewBag.ListCompany = new SelectList(MCompanyRepo.get(), "id", "name");
            if (ModelState.IsValid)
            {
                if(MEmployeeRepo.HitungEmplo(model) == true)
                {
                    var result = new
                    {
                        success = true,
                        alertType = "warning",
                        alertStrong = "Error!",
                        alertMessage = "Employee number already used"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MEmployeeRepo.HitungEmplo(model) == false)
                {
                    MEmployeeRepo.insert(model);
                    var result = new
                    {
                        success = false,
                        alertType = "success",
                        alertStrong = "Data Saved !",
                        alertMessage = "New Employee has been add with employee ID number" + model.employee_number

                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_Add", model);
        }

         public ActionResult Delete(int id)
        {
           MEmployeeVM model = MEmployeeRepo.getbyid(id);
            MEmployeeRepo.Delete(id);
            var result = new
            {
                success = true,
                alertType = "info",
                alertStrong = "Data Deleted !",
                alertMessage = "Data Deleted! Data Company With Employee ID Number" + model.employee_number + "Has been deleted"

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        

        public ActionResult Detail(int id)
        {
            MEmployeeVM model = MEmployeeRepo.GetDetail(id);
            ViewBag.Title = "View Company-" + model.first_name + model.last_name+ "(" + model.employee_number + ")";


            return PartialView(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListCompany = new SelectList(MCompanyRepo.get(), "id", "name");
            MEmployeeVM model = MEmployeeRepo.getbyid(id);
            ViewBag.Title = "Edit Company-" + model.first_name+model.last_name + "(" + model.employee_number + ")";
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MEmployeeVM model)
        {
            ViewBag.ListCompany = new SelectList(MEmployeeRepo.get(), "id", "name");
            if (MEmployeeRepo.HitungEmplo(model) == true)
            {
                var result = new
                {
                    success = true,
                    alertType = "warning",
                    alertStrong = "Error!",
                    alertMessage = "Employee number already used"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (MEmployeeRepo.HitungEmplo(model) == false)
            {
                MEmployeeRepo.update(model);
                var result = new
                {
                    success = false,
                    alertType = "success",
                    alertStrong = "Data Updated !",
                    alertMessage = "Data Updated! Data employee has been Updated"
                    
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return PartialView(model);
        }

        public JsonResult CheckNama(string nama)
        {
            var result = new
            {
                success = true,
                data = MEmployeeRepo.CheckNama(nama),
                alertType = "error",
                alertStrong = "Error !",
                alertMessage = "Your Data with name </strong>(" + nama + ")</strong>Has been created"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}