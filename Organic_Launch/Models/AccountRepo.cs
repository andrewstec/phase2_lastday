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
            account.password = password;//Add encryption? Do we need this field?
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
            db.SaveChanges();

            Account newUserAccount = db.Accounts.Where( u => u.username ==  newUser.UserName ).FirstOrDefault();
            AccountDetail newUserAccountDetail = new AccountDetail();
            int id = newUserAccount.accountID;

            Address address = db.Addresses.Where(a => a.addressID == 1).FirstOrDefault();
            address.city = "Vancouver";
            address.province = "BC";

            Farm farm = db.Farms.Where(f => f.farmID == 1).FirstOrDefault();
            farm.farmName = newUser.UserName = "Farm";
            farm.farmProfile = newUser.UserName = "FarmProfile";

            address.AccountDetails.Add(newUserAccountDetail);
            db.SaveChanges();

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