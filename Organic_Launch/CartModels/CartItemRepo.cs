using Organic_Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartItemRepo
    {
        public IEnumerable<CartItemVM> GetCartItemsBySession(string sessionId)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            IEnumerable<CartItemVM> cartItems =
                from p in db.CartProducts
                from pv in db.ProductVisits
                where p.productID == pv.productID && pv.sessionID == sessionId
                select new CartItemVM()
                {
                    ProductID = p.productID,
                    Name = p.productName,
                    Qty = (int)pv.qtyOrdered,
                    Price = (decimal)p.price,
                    CartItemTotal = (decimal)(pv.qtyOrdered * p.price)
                };
            return cartItems;
        }
    }
}