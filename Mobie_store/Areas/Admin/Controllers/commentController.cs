using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobie_store.Areas.Admin.Controllers
{ 
    public class CommentController : Controller
    {
        // GET: comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listComments()
        {
            return View();
        }
    }
}