﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Organic_Launch.Controllers
{
    public class ProductController : Controller
    {
        ProductRepo products = new ProductRepo();
        // GET: Product
        public ActionResult List()
        {
            ProductRepo products = new ProductRepo();
            return View(products.GetAllProducts());
        }

        public ActionResult Single(int id)
        {
            return View(products.GetProduct(id));
        }

        //Needs styling
        [HttpPost]
        public ActionResult Add(string productName, decimal price, string productCategory, string productDescription, int qtyInKG)
        {
            Product product = new Product();
            product.productName = productName;
            product.priceInKg = price;
            product.productCategory = productCategory;
            product.productDescription = productDescription;
            product.qtyInKG = qtyInKG;

            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("List");
        }
        public ActionResult Add()
        {
            return View();
        }

        //Needs styling
        public ActionResult Edit(int id)
        {
            return View(products.GetProduct(id));
        }

        [HttpPost]
        public ActionResult Edit(int productID, string productName, decimal price, string productCategory, string productDescription, int qty)
        {
            products.UpdateProduct(productID, productName, price, productCategory, productDescription, qty);
            return RedirectToAction("List");
        }

        //JSON API
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public ActionResult MyProducts(int id)
        {
            var products = ProductRepo.GetMyProducts(id);
            var json = JsonConvert.SerializeObject(products);
            return new ContentResult
            {
                Content = json.ToString(),
                ContentType = "application/json"
            };
        }
    }
}