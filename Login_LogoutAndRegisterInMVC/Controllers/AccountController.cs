using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_LogoutAndRegisterInMVC.Models;
using System.Web.Security;

namespace Login_LogoutAndRegisterInMVC.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model) 
        {
            using (var context = new AniketEntities())
            {
                bool isvalid = context.Users.Any(x => x.UserName == model.Username && x.password == model.password);
                if (isvalid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index","Employees");
                }

                ModelState.AddModelError("","Invalid username and password...");
                
            }
            return View();
        }

        // GET: Account
        public ActionResult SignUp() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            using (var context = new AniketEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login"); 
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}