using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        // GET: posts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listPosts()
        {
            using (var db = new MyDBContext())
            {
                List<post> _post = db.posts.ToList();
                return View(_post);
            }
                
        }
        [HttpGet]
        public ActionResult addNew()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult addNew(post obj)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDBContext())
                {
                    db.posts.Add(new post
                    {
                        name = obj.name,
                        product_id = obj.product_id,
                        descripton = obj.descripton
                });
                    db.SaveChanges();
                    ViewBag.HtmlStr = "<div class=\"alert alert-info alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Complete!.</span></div>";
                }
            }
            else
            {
                ViewBag.HtmlStr = "<div class=\"alert alert-rose alert-with-icon col-md-6 col-md-offset-3\" data-notify=\"container\"><i class=\"material-icons\" data-notify=\"icon\">notifications</i><button type = \"button\" aria-hidden=\"true\" class=\"close\"><i class=\"material-icons\">close</i></button><span data-notify=\"message\">Insert Error!.</span></div>";
            }
            return View();
        }
    }
}