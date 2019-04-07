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
    public class ImageController : Controller
    {
        // GET: image
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listImage()
        {
            using (var db = new MyDBContext())
            {
                List<image> image = db.images.ToList();
                image.ForEach(x => x.product.images.Clear());
                return View(image);
            }
                
        }
        [HttpGet]
        public ActionResult addNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addNew(image obj, List<HttpPostedFileBase> files)
        {
            using (var db = new MyDBContext())
            {
                if (ModelState.IsValid)
                {
                    foreach(HttpPostedFileBase img in files)
                    {
                        if(img != null && img.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(img.FileName);
                            var _ex = Path.GetExtension(img.FileName);
                            var path = Path.Combine(Server.MapPath("~/Upload/Products"), Hash.EncMD5(filename) + _ex);
                            img.SaveAs(path);
                            db.images.Add(new image {
                                product_id = obj.product_id,
                                url = "/Upload/Products/" + Hash.EncMD5(filename) + _ex
                            });
                            db.SaveChanges();
                            ViewBag.HtmlStr = "<div class=\"alert alert-info alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Complete!.</span></div>";
                            ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", filename);
                            
                        }
                    }
                }
                else
                {
                    ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
                }
                return View();
            }
        }
        public bool delete(int id)
        {
            using (var db = new MyDBContext())
            {
                var images = db.images.Find(id);
                db.images.Remove(images);
                db.SaveChanges();
                return true;
            }
        }

        [HttpPost]
        public JsonResult deleteImage(int id)
        {
            var _result = new ImageController().delete(id);
            return Json(new
            {
                status = _result
            });
        }
    }
}