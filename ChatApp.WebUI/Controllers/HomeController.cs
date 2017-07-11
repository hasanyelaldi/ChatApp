using ChatApp.Business;
using ChatApp.Business.Repository;
using ChatApp.Model.Core;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ChatAppContext context = new ChatAppContext();

        private IRepository<User> userRepository;

        private string isAdmin = "True";
        public HomeController(IRepository<User> repository)
        {
            userRepository = repository;
            bool result = context.Users.Any(u => u.UserName == "admin");
            if (result == false)
            {
                userRepository.Create(new User()
                {
                    UserName = "admin",
                    Password = "123456",
                    Image = "/Content/img/admin.jpg",
                    IsAdmin = true
                });
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }
        #region ----- User -----

        public ActionResult UserList(string searchString = null)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["IsAdmin"];
            if (myCookie.Value != isAdmin)
                return RedirectToAction("Index", "Home");

            var users = context.Users.Where(x => true);
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                users = users.Where(p => p.UserName.Contains(searchString));
            }
            users = users.OrderBy(x => x.UserName);
            return View(users);
        }

        public ActionResult UserCreate()
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["IsAdmin"];
            if (myCookie.Value != isAdmin)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult UserCreate(User user)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["IsAdmin"];
            if (myCookie.Value != isAdmin)
                return RedirectToAction("Index", "Home");

            try
            {
                if (Request.Files.Count > 0 && ModelState.IsValid)
                {
                    string filename = Guid.NewGuid().ToString().Replace("-", "");
                    string path = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string fullname = "~/Content/img/" + filename + path;
                    Request.Files[0].SaveAs(Server.MapPath(fullname));
                    user.Image = "/Content/img/" + filename + path;

                    userRepository.Create(user);
                    ViewBag.Result = true;
                    return RedirectToAction("UserList");
                }
                else
                {
                    ViewBag.Result = false;
                    return View(user);
                }
            }
            catch
            {
                ViewBag.Result = false;
                return View();
            }
        }

        public ActionResult UserEdit(int id)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["IsAdmin"];
            if (myCookie.Value != isAdmin)
                return RedirectToAction("Index", "Home");

            User user = userRepository.Find(id);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(User user)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["IsAdmin"];
            if (myCookie.Value != isAdmin)
                return RedirectToAction("Index", "Home");
            try
            {
                if (Request.Files.Count > 0 && ModelState.IsValid)
                {
                    string filename = Guid.NewGuid().ToString().Replace("-", "");
                    string path = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string fullname = "~/Content/img/" + filename + path;
                    Request.Files[0].SaveAs(Server.MapPath(fullname));
                    user.Image = "/Content/img/" + filename + path;

                    userRepository.Update(user);
                    ViewBag.Result = true;
                    return RedirectToAction("UserList");
                }
                else
                {
                    ViewBag.Result = false;
                    return View(user);
                }
            }
            catch
            {
                ViewBag.Result = false;
                return View();
            }
        }

        [HttpPost]
        public ActionResult UserDelete(int id)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["IsAdmin"];
            if (myCookie.Value != isAdmin)
                return RedirectToAction("Index", "Home");

            User user = userRepository.Find(id);

            if (user == null)
                return HttpNotFound();

            try
            {
                userRepository.Delete(user);
                return RedirectToAction("UserList");
            }
            catch
            {
                return View();
            }
        }
		
		
        #endregion

        //-10
    }
}