using Organic_Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ProductVisitRepo
    {
        public IEnumerable<ProductVisit> ShowProductVisits()
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            return db.ProductVisits.ToList();
        }

        public IEnumerable<ProductVisit> getProductVisit(string sessionId)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            var prodVisit = db.ProductVisits
                .Where(pv => pv.sessionID == sessionId);
            return prodVisit;
        }

        public void removeProductVisit(string sessionId)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            var prodVisit = (from pv in db.ProductVisits where pv.sessionID == sessionId select pv);
            if (prodVisit.Count() > 0)
            {
                db.ProductVisits.RemoveRange(prodVisit);
                db.SaveChanges();
            }
        }

        //Updates a productVisit entry if it already exists. Creates a new one otherwise
        public void addProductVisit(string sessionId, int productId, int qty, DateTime lastUpdated)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();

            int productVisit = db.ProductVisits
                .Where(prodVisit => prodVisit.sessionID == sessionId && prodVisit.productID == productId).Count();
            if (productVisit > 0)
            {
                var prodVisit = db.ProductVisits
                .Where(prVisit => prVisit.sessionID == sessionId && prVisit.productID == productId).First();
                db.ProductVisits.Remove(prodVisit);
                db.SaveChanges();
            }
           
            ProductVisit pv = new ProductVisit();
            pv.sessionID = sessionId;
            pv.productID = productId;
            pv.qtyOrdered = qty;
            pv.updated = lastUpdated;
            db.ProductVisits.Add(pv);
            db.SaveChanges();
        }
    }
}