using ChatApp.Business;
using ChatApp.Business.Repository;
using ChatApp.Model.Core;
using ChatApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatApp.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ChatAppContext context = new ChatAppContext();

        private IRepository<User> userRepository;
        IAuthentication authentication;

        public AccountController(IRepository<User> repository, IAuthentication authentication)
        {
            userRepository = repository;
            this.authentication = authentication;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                if (authentication.Authenticate(user.UserName, user.Password))
                {
                    var result = context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    HttpCookie cookie = new HttpCookie("IsAdmin", result.IsAdmin.ToString());
                    Response.Cookies.Add(cookie);

                    if (result.Image != null)
                    {
                        Session["Image"] = result.Image;
                    }


                    if (result.IsAdmin == true)
                        return RedirectToAction("UserList", "Home");
                    else
                        return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı yada şifre hatalı");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}