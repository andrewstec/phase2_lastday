using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartItemVM
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal CartItemTotal { get; set; }

        public CartItemVM() { }

        public CartItemVM(int productId, string name, int qty, decimal price)
        {
            ProductID = productId;
            Name = name;
            Qty = qty;
            Price = price;
            CartItemTotal = qty * price;
        }
    }
}