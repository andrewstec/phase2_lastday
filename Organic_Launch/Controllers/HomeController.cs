using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Organic_Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication3.BusinessLogic;
using WebApplication1.BusinessLayer;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {

        const string EMAIL_CONFIRMATION = "EmailConfirmation";
        const string PASSWORD_RESET = "ResetPassword";

        string GetUserRole(Login login)
        {
            FoodSaleAuthEntities context = new FoodSaleAuthEntities();
            var user = context.AspNetUsers.Where(u => u.UserName == login.UserName).FirstOrDefault();
            IQueryable<string> roleQuery = from u in context.AspNetUsers
                                           from r in u.AspNetRoles
                                           where u.UserName == login.UserName
                                           select r.Name;
            string[] roles = roleQuery.ToArray();
            if (roles != null)
            {
                return roles[0];
            }
            else
            {
                return null;
            }
        }


        void LockoutUntilYear3015(UserManager<IdentityUser> manager, IdentityUser identityUser)
        {

            if (identityUser != null)
            {
                identityUser.LockoutEnabled = true;
                identityUser.LockoutEndDateUtc = DateTime.UtcNow.AddYears(999);
                manager.UpdateAsync(identityUser);
            }

        }

        void DisableLockout(UserManager<IdentityUser> manager, IdentityUser identityUser)
        {
            if (identityUser != null)
            {
                identityUser.LockoutEnabled = true;
                identityUser.LockoutEndDateUtc = null;
                manager.UpdateAsync(identityUser);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddUserToRole()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddUserToRole(string userName, string roleName)
        {
            FoodSaleAuthEntities context = new FoodSaleAuthEntities();
            AspNetUser user = context.AspNetUsers
                             .Where(u => u.UserName == userName).FirstOrDefault();
            AspNetRole role = context.AspNetRoles
                             .Where(r => r.Name == roleName).FirstOrDefault();

            user.AspNetRoles.Add(role);
            context.SaveChanges();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddRole(AspNetRole role)
        {
            FoodSaleAuthEntities context = new FoodSaleAuthEntities();
            context.AspNetRoles.Add(role);
            context.SaveChanges();
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindByEmail(email);
            CreateTokenProvider(manager, PASSWORD_RESET);

            if (user != null)
            {
                var code = manager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Home",
                                             new { userId = user.Id, code = code },
                                             protocol: Request.Url.Scheme);
                string emailMessage = "Email successfully sent. Please check your email to reset your password";

                string response = new MailHelper().EmailFromArvixe(new ViewModels.Message(email, emailMessage));

                ViewBag.FakeEmailMessage = emailMessage;

                return View();
            }
           
            ViewBag.Error = "There wasn't an account for that email";
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string userID, string code)
        {
            ViewBag.PasswordToken = code;
            ViewBag.UserID = userID;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(string password, string passwordConfirm,
                                          string passwordToken, string userID)
        {

            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindById(userID);
            CreateTokenProvider(manager, PASSWORD_RESET);

            IdentityResult result = manager.ResetPassword(userID, passwordToken, password);
            if (result.Succeeded)
                ViewBag.Result = "The password has been reset.";
            else
                ViewBag.Result = "The password has not been reset.";
            return View();
        }


        public ActionResult ConfirmEmail(string userID, string code)
        {
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindById(userID);
            CreateTokenProvider(manager, EMAIL_CONFIRMATION);
            try
            {
                IdentityResult result = manager.ConfirmEmail(userID, code);
                if (result.Succeeded)
                    ViewBag.Message = "You are now registered!";
            }
            catch
            {
                ViewBag.Message = "Validation attempt failed!";
            }
            return View();
        }


        void CreateTokenProvider(UserManager<IdentityUser> manager, string tokenType)
        {
            manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();
        }

        bool ValidLogin(Login login)
        {
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(userStore)
            {
                UserLockoutEnabledByDefault = true,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),
                MaxFailedAccessAttemptsBeforeLockout = 3
            };
            var user = userManager.FindByName(login.UserName);

            if (user == null)
                return false;

            // User is locked out.
            if (userManager.SupportsUserLockout && userManager.IsLockedOut(user.Id))
            {
                return false;
            }

            // Validated user was locked out but now can be reset.
            if (userManager.CheckPassword(user, login.Password))
            {
                if (userManager.SupportsUserLockout
                 && userManager.GetAccessFailedCount(user.Id) > 0)
                {
                    userManager.ResetAccessFailedCount(user.Id);
                }
            }

            // Login is invalid so increment failed attempts.
            else {
                bool lockoutEnabled = userManager.GetLockoutEnabled(user.Id);
                if (userManager.SupportsUserLockout && userManager.GetLockoutEnabled(user.Id))
                {
                    userManager.AccessFailed(user.Id);
                    return false;
                }
            }
            return true;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            // UserStore and UserManager manages data retreival.
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            IdentityUser identityUser = manager.Find(login.UserName,
                                                             login.Password);

            //DisableLockout(manager, identityUser);
            //LockoutUntilYear3015(manager, identityUser);
            System.Threading.Thread.Sleep(1000);
            if (ModelState.IsValid)
            {
                if ((ValidLogin(login)))
                {
                    IAuthenticationManager authenticationManager
                                           = HttpContext.GetOwinContext().Authentication;
                    authenticationManager
                   .SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    var identity = new ClaimsIdentity(new[] {
                                            new Claim(ClaimTypes.Name, login.UserName),
                                        },
                                        DefaultAuthenticationTypes.ApplicationCookie,
                                        ClaimTypes.Name, ClaimTypes.Role);
                    // SignIn() accepts ClaimsIdentity and issues logged in cookie. 
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, identity);

                    // A redirect based on the user type to forward the user to the correct controller homepage
                    string userRole = GetUserRole(login);

                    if (userRole != null)
                    {
                        Session["UserType"] = userRole;
                        if (userRole.Equals("Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (userRole.Equals("Buyer"))
                        {
                            return RedirectToAction("Index", "Buyer");
                        }
                        else if (userRole.Equals("Farm"))
                        {
                            return RedirectToAction("Index", "Farm");
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.errorMsg = "Invalid Username or Password";
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisteredUser newUser)
        {
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore)
            {
                UserLockoutEnabledByDefault = true,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),
                MaxFailedAccessAttemptsBeforeLockout = 3
            };

            var identityUser = new IdentityUser()
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };
            IdentityResult result = manager.Create(identityUser, newUser.Password);

            if (result.Succeeded)
            {

                if (newUser.UserRole.Equals("Buyer") || newUser.UserRole.Equals("Farm"))
                {
                    //Taking the username on the account successful creation and applying it to the
                    //Farm database to create a Farm table with that username under the 'farmName' field.
                    AccountRepo accountRepo = new AccountRepo();
                    accountRepo.InitializeUserAccount(newUser);
                }

                var authenticationManager
                                  = HttpContext.Request.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(identityUser,
                                           DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { },
                                             userIdentity);
                string testVariable = newUser.UserRole;
                AddUserToRole(newUser.UserName, newUser.UserRole);
            }
            CaptchaHelper captchaHelper = new CaptchaHelper();
            string captchaResponse = captchaHelper.CheckRecaptcha();
            ViewBag.CaptchaResponse = captchaResponse;

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SecureArea()
        {
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}


