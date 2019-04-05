using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Mobie_store.Models.Entity;
using Mobie_store.Models.Function;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public string complete = "Insert Complete!";
        public string error = "Insert Error!";
        // GET: users
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listUsers()
        {
            using (var db = new MyDBContext())
            {
                List<user> users = db.users.ToList();
                return View(users);
            }
        }
        [HttpGet]
        public ActionResult addNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addNew(user obj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var _ex = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/Users"), Hash.EncMD5(filename)+_ex );
                    file.SaveAs(path);
                    using (var db = new MyDBContext())
                    {
                        var pwd = Hash.EncMD5(obj.pwd);
                        db.users.Add(new user
                        {
                            username = obj.username,
                            pwd = pwd, //encode MD5 
                            email = obj.email,
                            fullname = obj.fullname,
                            address = obj.address,
                            phone = obj.phone,
                            activated = 1,
                            image = "/Upload/Users/" + Hash.EncMD5(filename) +_ex
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
                var user = db.users.Find(id);
                if(user.activated == 1)
                {
                    user.activated = 0;
                    db.SaveChanges();
                    return st;
                }
                if(user.activated == 0)
                {
                    user.activated = 1;
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
                var user = db.users.Find(id);
                db.users.Remove(user);
                db.SaveChanges();
                return true;
            }
        }

        [HttpPost]
        public JsonResult deleteUser(int id)
        {
            var _result = new UsersController().delete(id);
            return Json(new {
                status = _result
            });
        }

        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            var result = new UsersController().status(id);
            return Json(new {
                status = result
            });
        }

        //[HttpGet]
        //public ActionResult activated(int id)
        //{
        //    using (var db = new MyDBContext())
        //    {
        //        int _update = db.Database.ExecuteSqlCommand("update users set activated = 1 where id=@p0", id);
        //    }
        //    return RedirectToAction("listUsers", "users");
        //}
        //[HttpGet]
        //public ActionResult deactivated(int id)
        //{
        //    using (var db = new MyDBContext())
        //    {
        //        int _update = db.Database.ExecuteSqlCommand("update users set activated = 0 where id=@p0", id);
        //    }
        //    return RedirectToAction("listUsers", "users");
        //}

        //[HttpGet]
        //public ActionResult delete(int id)
        //{
        //    using (var db = new MyDBContext())
        //    {
        //        int _delete = db.Database.ExecuteSqlCommand("delete from users where id =@p0", id);
        //    }
        //    return RedirectToAction("listUsers","users");
        //}
    }
}