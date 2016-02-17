using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Organic_Launch.Controllers
{
    public class AccountController : Controller
    {
        AccountRepo accounts = new AccountRepo();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }

        //NEEDS STYLING
        public ActionResult Edit(int id)
        {
            return View(accounts.GetAccount(id));
        }

        //WORKING. NEEDS TO REDIRECT TO CORRECT PAGE.
        [HttpPost]
        public ActionResult Edit(int id, string email)
        {
            accounts.UpdateAccount(id, email);
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        //NOT SURE IF FUNCTIONS BELOW ARE NEEDED

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            return View();
        }


    }
}