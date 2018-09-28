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
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult GetById(int id)
        {
            var data = new
            {
                obj = MCompanyRepo.getbyid(id)
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById2(int id)
        {
            var data = new
            {
                obj = MEmployeeRepo.getbyid(id)
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}