using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class AccountDetailRepo
    {
        public IEnumerable<AccountDetail> GetAllAccountDetails()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.AccountDetails.ToList();
        }

        public AccountDetail GetAccountDetail(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            AccountDetail accountDetail = db.AccountDetails.Where(ad => ad.accountDetailID == id).FirstOrDefault();
            return accountDetail;
        }

        public void AddAccountDetail(int accountID, int farmID, int addressID)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            AccountDetail accountDetail = new AccountDetail();
            accountDetail.accountID = accountID;
            accountDetail.farmID = farmID;
            accountDetail.addressID = addressID;
            db.AccountDetails.Add(accountDetail);
            db.SaveChanges();
        }

        public void RemoveAccountDetail(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            AccountDetail accountDetail = db.AccountDetails.Where(ad => ad.accountDetailID == id).FirstOrDefault();
            db.AccountDetails.Remove(accountDetail);
            db.SaveChanges();
        }
    }
}