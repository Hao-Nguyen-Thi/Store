using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new MyDBContext())
            {
                var product = db.products.ToList();
                ViewBag.product = product;
                List<image> lstimg = db.images.ToList();
                return View(lstimg);
            }
        }

    }
}