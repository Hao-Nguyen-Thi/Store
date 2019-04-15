using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Product(int id)
        {
            MyDBContext db = new MyDBContext();
            List<product> product = db.products.Where(m => m.product_cate_id == id).ToList();
            return View(product);
        }
    }
}