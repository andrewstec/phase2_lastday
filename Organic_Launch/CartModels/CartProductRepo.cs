using Organic_Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartProductRepo
    {
        public CartProductRepo ()
        {

        }

        public IEnumerable<CartProduct> ShowProducts()
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            return db.CartProducts.ToList();
        }

        public CartProduct getProduct(int id)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            CartProduct product = db.CartProducts.Where(p => p.productID == id).FirstOrDefault();
            return product;
        }
    }
}