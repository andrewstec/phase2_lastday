using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Security.Principal;
using WebApplication3.BusinessLogic;
using WebApplication3.Models;

namespace Organic_Launch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        void Application_PostAuthenticateRequest()
        {
            if (User.Identity.IsAuthenticated)
            {
                var name = User.Identity.Name; // Get current user name.

                FoodSaleAuthEntities context = new FoodSaleAuthEntities();
                var user = context.AspNetUsers.Where(u => u.UserName == name).FirstOrDefault();
                IQueryable<string> roleQuery = from u in context.AspNetUsers
                                               from r in u.AspNetRoles
                                               where u.UserName == Context.User.Identity.Name
                                               select r.Name;
                string[] roles = roleQuery.ToArray();

                HttpContext.Current.User = Thread.CurrentPrincipal =
                                           new GenericPrincipal(User.Identity, roles);
            }
        }

        protected void Application_Start()
        {
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionHelper helper = new SessionHelper();
            helper.Initialize();

            VisitRepo visit = new VisitRepo();
            visit.removeOldVisits(DateTime.Now);
            visit.addVisit(helper.SessionID, helper.Start);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.SessionID != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();

            }
        }
    }
}
