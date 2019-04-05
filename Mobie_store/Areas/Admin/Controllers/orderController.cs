using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobie_store.Models.Entity;

namespace Mobie_store.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: order
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listOrder()
        {
            using (var db = new MyDBContext())
            {
                List<order> order = db.orders.ToList();
                order.ForEach(x => x.user.orders.Clear());// lawpj vong tron
                return View(order);
            }
        }
    }
}