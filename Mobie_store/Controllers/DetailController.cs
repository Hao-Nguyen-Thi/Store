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
        public static List<image> getorder(int? id)
        {
            using (var db = SetupConnection.ConnectionFactory())
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                var sql = "SELECT * FROM dbo.image Where product_id =@id;";
                return db.Query<image>(sql, new { id = id }).ToList();
            }
        }
        // GET: Detail
        public ActionResult Index(int id)
        {
            MyDBContext db = new MyDBContext();
            product product = db.products.Find(id);
            List<image> img = DetailController.getorder(id);
            ViewBag.Img = img;
            return View(product);
        }
    }
}