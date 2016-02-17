using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class FarmerProfileVMRepo
    {
        public FarmerProfileVM GetFarmerProfile(int accountID)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            AccountDetail accountDetail = db.AccountDetails.Where(ad => ad.accountID == accountID).FirstOrDefault();
            Account account = db.Accounts.Where(a => a.accountID == accountID).FirstOrDefault();
            Address address = db.Addresses.Where(adr => adr.addressID == accountDetail.addressID).FirstOrDefault();
            Farm farm = db.Farms.Where(f => f.farmID == accountDetail.farmID).FirstOrDefault();
            FarmerProfileVM profile = new FarmerProfileVM();
            profile.Username = account.username;
            profile.Email = account.email;
            profile.StreetNum = address.streetNum;
            profile.StreetName = address.streetName;
            profile.Province = address.province;
            profile.City = address.city;
            profile.Zip = address.zip;
            profile.farmName = farm.farmName;
            profile.farmProfile = farm.farmProfile;
            return profile;
        }
    }
}