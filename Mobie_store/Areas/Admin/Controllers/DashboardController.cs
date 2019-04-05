using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;
using Mobie_store.Models.Function;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Dashboard");
            }
        }

        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Login", "Dashboard");
        }

        [HttpGet]
        public ActionResult profile()
        {
            if (Session["admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Dasboard");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(admin model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDBContext())
                {
                    var pwd = Hash.EncMD5(model.pwd);
                    var _admin = db.admins.Where(a => a.email.Equals(model.email) && a.pwd.Equals(pwd)).FirstOrDefault();
                    if (_admin != null)
                    {
                        Session["admin"] = _admin;
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            else
            {
                ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
                RedirectToAction("Login", "main");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(admin model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/Admin"), filename);
                    file.SaveAs(path);
                    using (var db = new MyDBContext())
                    {
                        db.admins.Add(new admin
                        {
                            username = model.username,
                            pwd = Hash.EncMD5(model.pwd),
                            email = model.email,
                            fullname = model.fullname,
                            address = model.address,
                            phone = model.phone,
                            level = 1,
                            image = "/Upload/Admin/" + filename
                        });
                        db.SaveChanges();
                        ViewBag.HtmlStr = "<div class=\"alert alert-info alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Complete!.</span></div>";
                    }
                }
                RedirectToAction("Login", "Dashboard");
            }
            else
            {
                RedirectToAction("Register", "Dashboard");
                ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
            }
            return View();
        }
    }
}