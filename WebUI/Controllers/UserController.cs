using Domain.Entities;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private EFUserRepository repository = new EFUserRepository();

        public ViewResult Login(string ReturnUrl)
        {
            TempData["href"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string UserPassword, string ReturnUrl)
        {
            if (UserName == "admin" && UserPassword == "admin")
            {
                FormsAuthentication.SetAuthCookie("admin", true);

                TempData["message"] = string.Format("Ви ввійшли під логіном admin");

                return Redirect("/");
            }
            else
            {
                var user = repository.GetAllUsers().FirstOrDefault(b => b.UserName == UserName && b.UserPassword == UserPassword);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                    //var ticket = new FormsAuthenticationTicket(2, user.UserId.ToString(), DateTime.Now, DateTime.Now.AddHours(1), true, "Admin");

                    ////if (user.IsAdmin == true)
                    ////{
                    ////    ticket = new FormsAuthenticationTicket(2, user.UserId.ToString(), DateTime.Now, DateTime.Now.AddHours(1), true, "Admin");
                    ////}

                    //var encTicket = FormsAuthentication.Encrypt(ticket);

                    //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    //cookie.Expires = DateTime.Now.AddHours(1);
                    //Response.Cookies.Add(cookie);

                    TempData["message"] = string.Format("Ви ввійшли під логіном \"{0}\"", user.UserName);

                    return Redirect("/");
                }
                else
                {
                    TempData["err-message"] = string.Format("Помилка авторизації");

                    TempData["href"] = ReturnUrl;
                    return View();
                }
            }
            
        }

        public void SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

        }
    }
}