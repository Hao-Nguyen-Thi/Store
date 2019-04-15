using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;
using Mobie_store.Models.Function;

namespace Mobie_store.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                user user = (user)Session["user"];
                if (user.activated == 1) {
                    return View(user);
                }
                else
                {
                    Session["user"] = null;
                    return RedirectToAction("Deni", "Customer");
                }
            }
            else
            {
                return RedirectToAction("Login","Customer");
            }
            
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(user obj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var _ex = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/Users"), Hash.EncMD5(filename) + _ex);
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
                            image = "/Upload/Users/" + Hash.EncMD5(filename) + _ex
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDBContext())
                {
                    var pwd = Hash.EncMD5(model.pwd);
                    var _user = db.users.Where(a => a.username.Equals(model.username) && a.pwd.Equals(pwd)).FirstOrDefault();
                    if (_user != null)
                    {
                        Session["user"] = _user;
                        return RedirectToAction("Index", "Customer");
                    }
                }
            }
            else
            {
                RedirectToAction("Login", "Customer");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Customer");
        }

        public ActionResult Deni()
        {
            return View();
        }
    }
}