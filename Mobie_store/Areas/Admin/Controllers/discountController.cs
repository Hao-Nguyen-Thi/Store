using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        // GET: discount
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listDiscount()
        {
            using (var db = new MyDBContext())
            {
                List<discount> discounts = db.discounts.ToList();
                return View(discounts);
            }
                
        }
        [HttpGet]
        public ActionResult addNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addNew(discount obj)
        {
            using (var db = new MyDBContext())
            {
                if (ModelState.IsValid)
                {
                    db.discounts.Add(obj);
                    db.SaveChanges();
                    ViewBag.HtmlStr = "<div class=\"alert alert-info alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Complete!.</span></div>";
                }
                else
                {
                    ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
                }
                return View();
            }
               
        }
    }
}