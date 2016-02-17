using Organic_Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.BusinessLogic;

namespace WebApplication3.Models
{
    public class VisitRepo
    {
        public IEnumerable<Visit> ShowVisits()
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            return db.Visits.ToList();
        }
        public Visit getVisit(string id)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            Visit visit = db.Visits
                .Where(v => v.sessionID == id)
                .FirstOrDefault();
            return visit;
        }
        public void removeVisit(string id)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            var visit = (from v in db.Visits where v.sessionID == id select v).FirstOrDefault();
            if ((from vis in db.Visits where vis.sessionID == id select vis).Count() > 0)
            {
                var productVisits = db.ProductVisits.Where(pv => pv.sessionID == visit.sessionID);
                db.ProductVisits.RemoveRange(productVisits);
                db.Visits.Remove(visit);
                db.SaveChanges();
            }
        }

        public void addVisit(string id, DateTime start)
        {
            ShoppingCartEntities db = new ShoppingCartEntities();
            Visit visit = new Visit();
            var prevVisit = db.Visits.SingleOrDefault(v => v.sessionID == id);
            if (prevVisit == null)
            {
                visit.sessionID = id;
                visit.started = start;
                db.Visits.Add(visit);
                db.SaveChanges();
            }
        }

        public void removeOldVisits(DateTime now) {
            ShoppingCartEntities db = new ShoppingCartEntities();
            var prodVisits = db.ProductVisits;
            DateTime oneHourAgo = DateTime.Now.AddHours(-1);
            List<ProductVisit> oldProdVisits = db.ProductVisits.Where(pv => DateTime.Compare((DateTime)pv.updated, oneHourAgo) < 0 ).ToList();
            //Remove from Visit and ProductVisit sessions that are older than 1 hour
            foreach (var pv in oldProdVisits)
            {
                var prodVisit = db.ProductVisits.Where(prodv => prodv.sessionID == pv.sessionID).FirstOrDefault();
                if (prodVisit != null)
                {
                    db.ProductVisits.Remove(prodVisit);
                    var visit = db.Visits.Where(v => v.sessionID == prodVisit.sessionID).FirstOrDefault();
                    if (visit != null)
                    {
                        db.Visits.Remove(visit);
                    }
                }
            }
            //Remove Visits that are older than 1 hour (sessions that didn't add anything to a cart)
            var Visits = db.Visits;
            List<Visit> oldVisits = db.Visits.Where(v => DateTime.Compare((DateTime)v.started, oneHourAgo) < 0).ToList();
            if (oldVisits.Count() > 0)
            {
                db.Visits.RemoveRange(oldVisits);
            }
            db.SaveChanges();
        }
    }
}