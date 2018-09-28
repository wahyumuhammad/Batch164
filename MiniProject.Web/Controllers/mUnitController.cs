using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniProject.Repo;
using MiniProject.Model;
using MiniProject.ViewModel;

namespace MiniProject.Web.Controllers
{
    public class mUnitController : Controller
    {
        AppEntity db = new AppEntity();



        // GET: mUnit
        public ActionResult Index()
        {
            
            ViewBag.ListCode = new SelectList(mUnitRepo.get(), "code", "code");
            ViewBag.ListName = new SelectList(mUnitRepo.get(), "name", "name");
            List<mUnitVM> data = mUnitRepo.get();
            return View(data);
        }

        public ActionResult List()
        {
            return PartialView("_List", mUnitRepo.get());
        }

        public JsonResult NameCheck(string nama)
        {
            var result = new
            {
                success = true,
                data = mUnitRepo.cekNama(nama),
                alerttype = "error",
                alertstrong = "error!",
                alertmessage = "unit name " + nama + " already exists!"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //ADD

        public ActionResult Add()
        {
            mUnitVM model = new mUnitVM();
            model.code = mUnitRepo.newCode();
            return PartialView("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(mUnitVM model)
        {

            if (ModelState.IsValid )
            {
                if (mUnitRepo.cekNama(model) == true)
                {
                    var result = new
                    {
                        success = false,
                        alertType = "error",
                        alertStrong = "Error!",
                        alertMessage = "Unit name " + model.name + " already exists!"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mUnitRepo.insert(model);
                    var result = new
                    {
                        success = true,
                        alertType = "info",
                        alertStrong = "Data Saved!",
                        alertMessage = "New Unit has been added with code (" + model.code + ")"
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                  
            }
            return PartialView("_Add",model);

        }

        public ActionResult Details(int id)
        {
            return PartialView("_Details", mUnitRepo.getById(id));
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", mUnitRepo.getById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mUnitVM model)
        {
            if (ModelState.IsValid )
            {
                if(mUnitRepo.cekNama(model) == true )
                {
                    var result = new
                    {
                        success = false,
                        alertType = "error",
                        alertStrong = "Error!",
                        alertMessage = "Unit name " + model.name + " already exists!"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                } else 
                {
                    mUnitRepo.update(model);
                    var result = new
                    { 
                        success = true,
                        alertType = "info",
                        alertStrong = "Updated!",
                        alertMessage = "Data unit has been updated!"
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                                
            }
            return PartialView("_Edit", model);
           
        }

        public ActionResult HideUnit(int id)
        {
            if (mUnitRepo.HideUnit(id) == true)
            {
                var result = new
                {
                    success = true,
                    message = "Unit has been deleted"
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }

        

        public ActionResult Search(mUnitVM model)
        {
            var data = new List<mUnitVM>();
            if (data.Count == 0 && model.code == null && model.name == null && model.createdDate == DateTime.MinValue && model.createdBy==0)
            {
                data = mUnitRepo.get();
            }
            else
            {
                data = mUnitRepo.Search(model);
            }
            return PartialView("_List", data);
        }

       
    }
}