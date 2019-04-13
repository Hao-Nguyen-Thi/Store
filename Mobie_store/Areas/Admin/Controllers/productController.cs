using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;
using Mobie_store.Models.Function;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listProduct()
        {
            using (var db = new MyDBContext())
            {
                List<product> product = db.products.ToList();
                return View(product);
            }
            
        }
        [HttpGet]
        public ActionResult addNew()
        {
            using (var db = new MyDBContext())
            {
                List<category> cate = db.categories.ToList();
                return View(cate);
            }
        }
        [HttpPost]
        public ActionResult addNew(product model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var _ex = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/SingleProduct"), Hash.EncMD5(filename) + _ex);
                    file.SaveAs(path);
                    using (var db = new MyDBContext())
                    {
                        db.products.Add(new product
                        {
                            sku = model.sku,
                            name = model.name,
                            price = model.price,
                            Ghz = model.Ghz,
                            color = model.color,
                            sensor = model.sensor,
                            cpu = model.cpu,
                            ram = model.ram,
                            storage = model.storage,
                            camera_front = model.camera_front,
                            camera_rear = model.camera_rear,
                            battery = model.battery,
                            display = model.display,
                            sim = model.sim,
                            status = model.status,
                            product_cate_id = model.product_cate_id,
                            image = "/Upload/SingleProduct/" + Hash.EncMD5(filename) + _ex,
                            activate = 1
                        });
                        db.SaveChanges();
                        ViewBag.HtmlStr = "<div class=\"alert alert-info alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Complete!.</span></div>";
                    }
                }
            }
            else
            {
                ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
            }
            return View();
        }
        public bool status(int id)
        {
            using (var db = new MyDBContext())
            {
                var st = false;
                var products = db.products.Find(id);
                if (products.activate == 1)
                {
                    products.activate = 0;
                    db.SaveChanges();
                    return st;
                }
                if (products.activate == 0)
                {
                    products.activate = 1;
                    db.SaveChanges();
                    st = true;
                }
                return st;
            }
        }
        public bool delete(int id)
        {
            using (var db = new MyDBContext())
            {
                var product = db.products.Find(id);
                db.products.Remove(product);
                db.SaveChanges();
                return true;
            }
        }
        [HttpPost]
        public JsonResult deleteProduct(int id)
        {
            var _result = new ProductController().delete(id);
            return Json(new
            {
                status = _result
            });
        }

        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            var result = new ProductController().status(id);
            return Json(new
            {
                status = result
            });
        }
    }
}