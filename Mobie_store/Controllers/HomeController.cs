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
            MyDBContext db = new MyDBContext();
            List<product> product = db.products.ToList();
            return View(product);
        }
    }
}