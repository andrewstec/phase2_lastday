﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.BusinessLogic;
using Organic_Launch;
using WebApplication3.Models;

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

        [HttpPost]
        public ActionResult ViewCart(int productId, int qty)
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
            pvRepo.addProductVisit(id, productId, qty, DateTime.Now);

            return RedirectToAction("MyCart", "Cart");
        }

        public ActionResult MyCart()
        {
            ShoppingCartEntities db = new ShoppingCartEntities();

            SessionHelper session = new SessionHelper();
            string id = session.SessionID;

            CartItemRepo cir = new CartItemRepo();
            return View(cir.GetCartItemsBySession(id));
        }

        public ActionResult CancelOrder()
        {
            SessionHelper session = new SessionHelper();
            string id = session.SessionID;

            ProductVisitRepo pvr = new ProductVisitRepo();
            pvr.removeProductVisit(id);

            //VisitRepo visitRepo = new VisitRepo();
            //visitRepo.removeVisit(id);

            return RedirectToAction("Index", "Cart");
        }
    }
}