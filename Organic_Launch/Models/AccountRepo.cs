using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;
using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public class AccountRepo
    {
        public IEnumerable<Account> GetAllAccounts()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.Accounts.ToList();
        }

        public Account GetAccount(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Account account = db.Accounts.Where(a => a.accountID == id).FirstOrDefault();
            return account;
        }

        //Need to add encryption to password
        public void AddAccount(string username, string password, string email, string accountType)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();

            Account account = new Account();
            account.username = username;
            account.email = email;
            account.accountType = accountType;

            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void RemoveAccount(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Account account = db.Accounts.Where(a => a.accountID == id).FirstOrDefault();
            db.Accounts.Remove(account);
            db.SaveChanges();
        }

        public void UpdateAccount(int id, string email)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Account account = db.Accounts.Where(a => a.accountID == id).FirstOrDefault();
            if (account != null)
            {
                account.email = email;
                db.SaveChanges();
            }
        }
        public void InitializeUserAccount(RegisteredUser newUser)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Account account = new Account()
            {
                username = newUser.UserName,
                email = newUser.Email,
                accountType = newUser.UserRole
            };

            db.Accounts.Add(account);

            Address address = new Address()
            {
                city = newUser.City,
                province = newUser.Province,
                streetName = newUser.StreetName,
                streetNum = newUser.StreetNum,
                zip = newUser.PostalCode
            };

            db.Addresses.Add(address);

            Farm farm = new Farm();
            if (newUser.UserRole.Equals("Farm"))
            {
                farm.farmName = newUser.FarmName;
                farm.farmProfile = newUser.FarmProfile;
                db.Farms.Add(farm);
            }
            db.SaveChanges();

            Account newUserAccount = db.Accounts.Where(u => u.username == newUser.UserName).FirstOrDefault();


            int accountID = newUserAccount.accountID;

            //querying new Address and Farm entities that we just added into sepearte tables.
            Address addressQuery = db.Addresses.Where(a => a.streetNum == newUser.StreetNum && a.streetName == newUser.StreetName && a.zip == newUser.PostalCode).FirstOrDefault();
            Farm farmQuery = db.Farms.Where(f => f.farmName == newUser.FarmName && f.farmProfile == newUser.FarmProfile).FirstOrDefault();

            //merging Address and Farm entities under the Account through the AccountDetails table
            AccountDetail newUserAccountDetail = new AccountDetail();
            int addressID = addressQuery.addressID;

            if (farmQuery != null)
            {

                int farmID = farmQuery.farmID;
                newUserAccountDetail.farmID = farmID;
            }

            newUserAccountDetail.accountID = accountID;
            newUserAccountDetail.addressID = addressID;

            db.AccountDetails.Add(newUserAccountDetail);
            db.SaveChanges();

            //farm.farmName = newUser.UserName = newUser.FarmName;
            //farm.farmProfile = newUser.UserName = newUser.FarmProfile;

            //address.AccountDetails.Add(newUserAccountDetail);
            //db.SaveChanges();

            //account.AccountDetails.Add(newUserAccountDetails);
            //Address newUserAddress = new Address();
            //Farm farm = new Farm();
            //farm.farmName = " farm name " + newUser.UserName;
            //newUserAddress.city = "Vancouver";
            //newUserAddress.AccountDetails.Add(newUserAccountDetails);
            //farm.AccountDetails.Add(newUserAccountDetails);
            db.SaveChanges();


            //if ( newUser.UserRole.Equals("Farm"))
            //{
            //    Farm farm = new Farm();
            //    farm.farmName = newUser.UserName;
            //    AccountDetail
            //}

            //if (account != null)
            //{
            //    account.email = email;
            //    db.SaveChanges();
            //}
        }
    }
}