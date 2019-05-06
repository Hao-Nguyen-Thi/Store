using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Controllers
{
    public class ListProductController : Controller
    {
        // GET: ListProduct
        public ActionResult Index()
        {
            using (var db = new MyDBContext())
            {
                List<product> product = db.products.ToList();
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Search(string Desc)
        {
            MyDBContext db = new MyDBContext();
            IQueryable<product> products = db.products.Where(product => product.name.Contains(Desc));
            var result = products.ToList();
            return View(result);
        }
    }
}