using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class AddressRepo
    {
        public IEnumerable<Address> GetAllAddresses()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.Addresses.ToList();
        }

        public Address GetAddress(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Address address = db.Addresses.Where(a => a.addressID == id).FirstOrDefault();
            return address;
        }

        public void AddAddress (string streetName, string streetNum, string province, string city, string zip)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Address address = new Address();
            address.streetName = streetName;
            address.streetNum = streetNum;
            address.province = province;
            address.city = city;
            address.zip = zip;

            db.Addresses.Add(address);
            db.SaveChanges();
        }

        public void RemoveAddress(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Address address = db.Addresses.Where(a => a.addressID == id).FirstOrDefault();
            db.Addresses.Remove(address);
            db.SaveChanges();
        }

        public void UpdateAddress(int id, string streetName, string streetNum, string province, string city, string zip)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Address address = db.Addresses.Where(a => a.addressID == id).FirstOrDefault();
            if (address != null)
            {
                address.streetName = streetName;
                address.streetNum = streetNum;
                address.province = province;
                address.city = city;
                address.zip = zip;
                db.SaveChanges();
            }
            
        }
    }
}