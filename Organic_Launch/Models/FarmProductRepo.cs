using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class FarmProductRepo
    {
        public IEnumerable<FarmProduct> GetAllFarmProducts()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.FarmProducts.ToList();
        }

        public FarmProduct GetFarmProduct(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            FarmProduct farmProduct = db.FarmProducts.Where(fp => fp.farmProductID == id).FirstOrDefault();
            return farmProduct;
        }

        public void AddFarmProduct(int farmID, int productID)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            FarmProduct farmProduct = new FarmProduct();
            farmProduct.farmID = farmID;
            farmProduct.productID = productID;
            db.FarmProducts.Add(farmProduct);
            db.SaveChanges();
        }

        public void RemoveFarmProduct(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            FarmProduct farmProduct = db.FarmProducts.Where(fp => fp.farmProductID == id).FirstOrDefault();
            db.FarmProducts.Remove(farmProduct);
            db.SaveChanges();
        }
    }
}