using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Organic_Launch.Controllers
{
    public class FarmController : Controller
    {
        FarmRepo farms = new FarmRepo();
        private FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
        // GET: Farmer

        [Authorize(Roles ="Admin, Buyer, Farm")]
        public ActionResult Index()
        {
            return View();
        }
        
        //Needs styling
        //Deleted and remade this view. Need to add styling again to this view
        [Authorize(Roles = "Admin, Buyer, Farm")]
        public ActionResult List()
        {
            return View(farms.GetAllFarms());
        }

        [Authorize(Roles = "Admin, Farm")]
        public ActionResult Details(int id)
        {
            return View(farms.GetFarm(id));
        }

        //Needs styling
        [Authorize(Roles = "Farm")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string farmName, string farmProfile)
        {
            farms.AddFarm(farmName, farmProfile);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "Farm")]
        public ActionResult Edit(int id)
        {
            return View(farms.GetFarm(id));
        }

        [Authorize(Roles = "Farm")]
        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }

        //Needs styling, Need to add the POST FUNCTION FOR THIS
        [Authorize(Roles = "Farm")]
        public ActionResult RemoveFarm(int id)
        {
            return View(farms.GetFarm(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            farms.RemoveFarm(id);
            return RedirectToAction("List");
        }
        
        public ActionResult Single(int id)
        {
            return View(farms.GetFarm(id));
        }
    }
}