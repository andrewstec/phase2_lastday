using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Organic_Launch.Controllers
{
    public class AdminController : Controller
    {
        private FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
        // GET: Admin

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //Need styling
        public ActionResult ListAccounts()
        {
            AccountRepo accounts = new AccountRepo();
            return View(accounts.GetAllAccounts());
        }

        public ActionResult ListFarmAccounts()
        {
            FarmRepo farms = new FarmRepo();
            var listOfFarms = farms.GetAllFarms();
            return View(listOfFarms);
        }

        //Have to delete all child tables that have the userID tied to it first.
        //NEED TO FIX THE REMOVE ACCOUNT FUNCTION IN ACCOUNT REPO
        public ActionResult DeleteAccount(int id)
        {
            AccountRepo accounts = new AccountRepo();
            accounts.RemoveAccount(id);
            return RedirectToAction("ListAccounts");
        }
    }
}