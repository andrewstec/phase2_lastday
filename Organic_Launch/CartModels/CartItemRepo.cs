using Organic_Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartItemRepo
    {
        public IEnumerable<CartItemVM> GetCartItemsByUser(string username)
        {
            FarmSaleDBEntities1 farmDB = new FarmSaleDBEntities1();
            var products = farmDB.Products.AsEnumerable();

            ShoppingCartEntities db = new ShoppingCartEntities();
            IEnumerable<CartItemVM> cartItems =
                from p in products
                from pv in db.ProductVisits
                where p.productID == pv.productID && pv.username == username
                select new CartItemVM()
                {
                    ProductID = p.productID,
                    Name = p.productName,
                    Qty = (int)pv.qtyOrdered,
                    Price = (decimal)p.priceInKg,
                    CartItemTotal = (decimal)(pv.qtyOrdered * p.priceInKg)
                };
            return cartItems;
        }
    }
}