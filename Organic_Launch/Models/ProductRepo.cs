using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class ProductRepo
    {
        public IEnumerable<Product> GetAllProducts()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.Products.ToList();
        }

        public Product GetProduct (int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Product product = db.Products.Where(p => p.productID == id).FirstOrDefault();
            return product;
        }

        public void AddProduct(string name, decimal price, string category, string description, int qty)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();

            Product product = new Product();
            product.productName = name;
            product.priceInKg = price;
            product.productCategory = category;
            product.productDescription = description;
            product.qty = qty;

            db.Products.Add(product);
            db.SaveChanges();
        }

        public void RemoveProduct (int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Product product = db.Products.Where(p => p.productID == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public void UpdateProduct(int id, string name, decimal price, string category, string description, int qty)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Product product = db.Products.Where(p => p.productID == id).FirstOrDefault();
            if (product != null)
            {
                product.productName = name;
                product.priceInKg = price;
                product.productCategory = category;
                product.productDescription = description;
                product.qty = qty;
                db.SaveChanges();
            }
        }

        public IEnumerable<Product> GetProductsByFarm(int farmID)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            List<FarmProduct> farmProducts = db.FarmProducts.Where(fp => fp.farmID == farmID).ToList();
            List<Product> products = new List<Product>();
            foreach (var farmProduct in farmProducts)
            {
                products.Add(db.Products.Where(p => p.productID == farmProduct.productID).FirstOrDefault());
            }
            return products;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            List<Product> products = db.Products.Where(p => p.productCategory == category).ToList();
            return products;
        }

        public static IEnumerable<ProductVM> GetMyProducts(int farmID)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            IEnumerable<ProductVM> products =
                from f in db.Farms
                from fp in db.FarmProducts
                where farmID == fp.farmID && f.farmID == farmID
                from p in db.Products
                where p.productID == fp.productID
                select new ProductVM()
                {
                    ProductName = p.productName,
                    Price = (decimal)p.priceInKg,
                    ProductCategory = p.productCategory,
                    Description = p.productDescription,
                    Qty = (int)p.qty,
                    FarmName = f.farmName
                };
            return products;
        }
    }
}