using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index(int a)
        {
            using (var db = new MyDBContext())
            {
                product product = db.products.SingleOrDefault(x=>x.id == a);
                return View(product);
            }
               
        }
        public ActionResult img()
        {
            using (var db = new MyDBContext())
            {
                var image = db.images.ToList();
                ViewBag.image = image;
                List<product> lstproduct = db.products.ToList();
                return View(lstproduct);
            }

        }
    }
}