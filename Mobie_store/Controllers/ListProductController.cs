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
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            using (var db = new MyDBContext())
            {
                IQueryable<product> products = db.products.Where(product => product.name.Contains(searchName));
                var result = products.ToList();
                return View();
            }
        }

    }
}