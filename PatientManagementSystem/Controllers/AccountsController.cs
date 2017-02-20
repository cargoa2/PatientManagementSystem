using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientManagementSystem.Models;
using System.Web.Security;

namespace PatientManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        private PatientContext db = new PatientContext();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount model, string returnUrl)
        {
            using (PatientContext db = new PatientContext())
            {
                var usr = db.UserAccount.Single(u => u.UserName == model.UserName && u.Password == model.Password);
                if (usr != null)
                {
                    FormsAuthentication.SetAuthCookie(usr.UserName, false);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/||"))
                    {
                        
                        return Redirect(returnUrl);
                    }
                else
                {
                        return RedirectToAction("Index", "Patients");
                        //return RedirectToAction("Login");
                }
                //Session["UserID"] = usr.UserId.ToString();
                //Session["UserName"] = usr.UserName.ToString();
                //Session["Users"] = usr.UserName.ToString();


                //return RedirectToAction("LoggedIn");
                //return RedirectToAction("Index", "Patients");

            }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect.");
                    return View();
                }

            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }

        //public ActionResult LoggedIn()
        //{
        //    if(Session["UserId"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}


       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
