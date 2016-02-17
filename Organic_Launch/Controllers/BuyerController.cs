using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Organic_Launch.Controllers
{
    public class BuyerController : Controller
    {
    
        private FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
        // GET: Product
        [Authorize(Roles = "Admin, Buyer, Farm")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Buyer, Farm")]
        public ActionResult List()
        {
            return View(db.Products.ToList());
        }

        [Authorize(Roles = "Admin, Farm")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Farm")]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Farm")]
        [HttpPost]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin, Farm")]
        public ActionResult Delete()
        {
            return View();
        }
    }
}