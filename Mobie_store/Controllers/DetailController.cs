using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;
using Dapper;
using Mobie_store.Hellper;
using System.Data;

namespace Mobie_store.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index(int id)
        {
            MyDBContext db = new MyDBContext();
            product product = db.products.Find(id);
            return View(product);
        }
    }
}