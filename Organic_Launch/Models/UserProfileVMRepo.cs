using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class UserProfileVMRepo
    {
        //Users
        public UserProfileVM GetUserProfile(int accountID)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            AccountDetail accountDetail = db.AccountDetails.Where(ad => ad.accountID == accountID).FirstOrDefault();
            Account account = db.Accounts.Where(a => a.accountID == accountID).FirstOrDefault();
            Address address = db.Addresses.Where(adr => adr.addressID == accountDetail.addressID).FirstOrDefault();
            UserProfileVM profile = new UserProfileVM();
            profile.Username = account.username;
            profile.Email = account.email;
            profile.StreetNum = address.streetNum;
            profile.StreetName = address.streetName;
            profile.Province = address.province;
            profile.City = address.city;
            profile.Zip = address.zip;
            return profile;
        }
    }
}