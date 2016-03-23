using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductVM
    {
        public string ProductName { get; set; }
        [DisplayName("Price In Kg")]
        public decimal Price { get; set; }
        public string ProductCategory { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public string FarmName { get; set; }

        public ProductVM() { }

        public ProductVM(string name, decimal price, string category, string description, int qty, string farmName)
        {
            ProductName = name;
            Price = price;
            ProductCategory = category;
            Description = description;
            Qty = qty;
            FarmName = farmName;
        }
    }
}