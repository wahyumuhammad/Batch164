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
    
    public class MCompanyController : Controller
    {
        // GET: m_company
        public ActionResult Index()
        {
            ViewBag.Title = "List Company";
            ViewBag.ListCompanyCode = new SelectList(MCompanyRepo.get(), "code", "code");
            ViewBag.ListCompanyName = new SelectList(MCompanyRepo.get(), "name", "name");
            List<MCompanyVM> item = MCompanyRepo.get();
            return View(item);
        }

        public ActionResult List()
        {
            List<MCompanyVM> item = MCompanyRepo.get();
            return PartialView("_List", item);
        }

        public ActionResult Add()
        {
            MCompanyVM model = new MCompanyVM();
            model.code = MCompanyRepo.KodeAuto();
            ViewBag.Title = "Add";
            return PartialView("_Add",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MCompanyVM model)
        {
            if(ModelState.IsValid)
            {
                if (MCompanyRepo.CheckCompany(model) == true)
                {
                    var result = new
                    {
                        success = true,
                        alertType = "warning",
                        alertStrong = "Error !",
                        alertMessage = "Company Already Created"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MCompanyRepo.CheckCompany(model) == false)
                {
                    MCompanyRepo.insert(model);
                    var result = new
                    {
                        success = false,
                        alertType = "success",
                        alertStrong = "Data Saved !",
                        alertMessage = "New company has been add with Code" + model.code
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return PartialView("_Add",model);
        }

        public ActionResult Detail(int id)
        {
            MCompanyVM model = MCompanyRepo.GetDetail(id);
            ViewBag.Title = "View Company-" +model.name + "(" + model.code + ")";
               
                
            return PartialView(model);
        }

        public ActionResult Edit (int id)
        {

            MCompanyVM model = MCompanyRepo.getbyid(id);
            
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MCompanyVM model)
        {
            if (ModelState.IsValid)
            {
                if (MCompanyRepo.CheckCompany(model) == true)
                {
                    var result = new
                    {
                        success = true,
                        alertType = "warning",
                        alertStrong = "Error !",
                        alertMessage = "Company Already Created"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (MCompanyRepo.CheckCompany(model) == false)
                {
                    MCompanyRepo.update(model);
                    var result = new
                    {
                        success = false,
                        alertType = "success",
                        alertStrong = "Data Updated !",
                        alertMessage = "Data company has been Updated"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return PartialView(model);
        }

        public ActionResult Delete(int id)
        {
            MCompanyVM model = MCompanyRepo.getbyid(id);
            if (MEmployeeRepo.Hitung(model.id) == true)
            {
                var result = new
                {
                    success = true,
                    alertType = "warning",
                    alertStrong = "Error!",
                    alertMessage = "Company still has employees"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (MEmployeeRepo.Hitung(model.id) == false)
            {
                MCompanyRepo.Delete(model);
                var result = new
                {
                    success = false,
                    alertType = "success",
                    alertStrong = "Data Deleted !",
                    alertMessage = "Data Deleted! Data Company With Code" + model.code + "Has been delete"
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            return PartialView("_Delete", model);
        }

        public JsonResult CheckNama(string nama)
        {
            var result = new
            {
                success = true,
                data = MCompanyRepo.CheckNameCompany(nama),
                alertType = "error",
                alertStrong = "Error !",
                alertMessage = "Your Data with name </strong>(" + nama + ")</strong>Has been created"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        //[HttpPost]
        //public ActionResult Delete(m_companyVM model)
        //{
           

        //    return PartialView("_Delete",model);
        //}

        //public ActionResult SearchData(m_companyVM model)
        //{
        //    var data = m_companyRepo.SearchData(model);
        //    if (data.Count == 0 && model.code == null && model.name == null && model.created_date == DateTime.MinValue && model.created_by == 0)
        //    {
        //        data = m_companyRepo.get();
        //    }

        //    return PartialView("_List", data);
        //}
    }
}