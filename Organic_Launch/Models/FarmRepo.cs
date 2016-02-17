using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class FarmRepo
    {

        public void InitializeFarmAccount(string username)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Farm farm = new Farm()
            {
                farmName = username,
                farmProfile = username + "profile"
            };

            if (farm != null)
            {
                db.Farms.Add(farm);
                db.SaveChanges();
            }
        }


        public IEnumerable<Farm> GetAllFarms()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.Farms.ToList();
        }

        public Farm GetFarm(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Farm farm = db.Farms.Where(f => f.farmID == id).FirstOrDefault();
            return farm;
        }

        public void AddFarm(string name, string profile)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();

            Farm farm = new Farm();
            farm.farmName = name;
            farm.farmProfile = profile;

            db.Farms.Add(farm);
            db.SaveChanges();
        }

        public void RemoveFarm(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Farm farm = db.Farms.Where(f => f.farmID == id).FirstOrDefault();
            db.Farms.Remove(farm);
            db.SaveChanges();
        }

        public void UpdateFarm(int id, string name, string profile)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Farm farm = db.Farms.Where(f => f.farmID == id).FirstOrDefault();
            if (farm != null)
            {
                farm.farmName = name;
                farm.farmProfile = profile;
                db.SaveChanges();
            }
        }
    }
}