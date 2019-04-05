using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: category
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listCategory()
        {
            using (var db = new MyDBContext())
            {
                List<category> cate = db.categories.ToList();
                return View(cate);
            }
                
        }
        [HttpGet]
        public ActionResult addNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addNew(category model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDBContext())
                {
                    db.categories.Add(model);
                    db.SaveChanges();
                    ViewBag.HtmlStr = "<div class=\"alert alert-info alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Complete!.</span></div>";
                }
            }
            else
            {
                ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
            }
            return View();
        }

        public bool delete(int id)
        {
            using (var db = new MyDBContext())
            {
                var cate = db.categories.Find(id);
                db.categories.Remove(cate);
                db.SaveChanges();
                return true;
            }
        }

        [HttpPost]
        public JsonResult deleteCate(int id)
        {
            var _result = new CategoryController().delete(id);
            return Json(new
            {
                status = _result
            });
        }
    }
}