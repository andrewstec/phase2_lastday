using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.BusinessLogic;
using Organic_Launch;
using WebApplication3.Models;
using WebApplication1.Models;

namespace Organic_Launch.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            CartProductRepo products = new CartProductRepo();
            return View(products.ShowProducts());
        }

        public ActionResult Thankyou()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            if (TempData["error"] != null)
            {
                ViewBag.Error = TempData["error"].ToString();
            }
            CartProductRepo products = new CartProductRepo();
            ShoppingCartEntities db = new ShoppingCartEntities();
            SessionHelper session = new SessionHelper();
            //Stores qty of product added in the cart for display in the view
            var prodVisit = db.ProductVisits.Where(pv => pv.productID == id && pv.sessionID == session.SessionID).FirstOrDefault();
            ViewBag.Qty = (prodVisit != null) ? prodVisit.qtyOrdered : 1;
            return View(products.getProduct(id));
        }

        public ActionResult AddProduct(int id)
        {
            ProductRepo products = new ProductRepo();
            ShoppingCartEntities db = new ShoppingCartEntities();
            SessionHelper session = new SessionHelper();
            var prodVisit = db.ProductVisits.Where(pv => pv.productID == id && pv.sessionID == session.SessionID).FirstOrDefault();
            ViewBag.Qty = (prodVisit != null) ? prodVisit.qtyOrdered : 1;
            return View(products.GetProduct(id));
        }

        [HttpPost]
        public ActionResult ViewCart(int productId, int qty, string username)
        {
            if (qty < 1)
            {
                TempData["error"] = "A positive quantity must be used";
                return RedirectToAction("Add", "Cart", new { id = productId });
            }

            ShoppingCartEntities db = new ShoppingCartEntities();

            SessionHelper session = new SessionHelper();
            string id = session.SessionID;

            ProductVisitRepo pvRepo = new ProductVisitRepo();
            pvRepo.addProductVisit(id, productId, qty, DateTime.Now, username);

            return RedirectToAction("MyCart", "Cart");
        }

        public ActionResult MyCart()
        {
            ShoppingCartEntities db = new ShoppingCartEntities();

            SessionHelper session = new SessionHelper();
            //string id = session.SessionID;

            CartItemRepo cir = new CartItemRepo();
            return View(cir.GetCartItemsByUser(User.Identity.Name));
        }

        public ActionResult CancelOrder()
        {
            SessionHelper session = new SessionHelper();
            string id = session.SessionID;

            ProductVisitRepo pvr = new ProductVisitRepo();
            pvr.removeProductVisit(id);

            //VisitRepo visitRepo = new VisitRepo();
            //visitRepo.removeVisit(id);

            return RedirectToAction("Products", "Cart");
        }

        public ActionResult Products()
        {
            ProductRepo products = new ProductRepo();
            return View(products.GetAllProducts());
        }
    }
}