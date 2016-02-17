using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class OrderRepo
    {
        public IEnumerable<Order> GetAllOrders()
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            return db.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Order order = db.Orders.Where(o => o.orderID == id).FirstOrDefault();
            return order;
        }

        public void AddOrder(int accountID, int farmProductID, int qty)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Order order = new Order();
            order.accountID = accountID;
            order.farmProductID = farmProductID;
            order.qty = qty;
            db.Orders.Add(order);
            db.SaveChanges();
        }
        
        public void RemoveOrder(int id)
        {
            FarmSaleDBEntities1 db = new FarmSaleDBEntities1();
            Order order = db.Orders.Where(o => o.orderID == id).FirstOrDefault();
            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }
}