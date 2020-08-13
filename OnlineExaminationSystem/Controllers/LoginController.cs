using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using OnlineExaminationSystem;
using OnlineExaminationSystem.Models;
using System.Web;
using System;
using OnlineExaminationSystem.App_Start;

namespace OnlineExaminationSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private OESContext db = new OESContext();
        public ActionResult Login(string returnUrl)
        {
            string username = "", password = "";
            bool remember = false;
            if (Request.Cookies["OESUserSettings"] != null)
            {
                if (Request.Cookies["OESUserSettings"]["UserName"] != null)
                {
                    username = Request.Cookies["OESUserSettings"]["UserName"];
                    password = Request.Cookies["OESUserSettings"]["Password"];
                    remember = true;
                }
            }
            var model = new LoginModel
            {
                UserName = username,
                Password = password,
                RememberMe = remember
            };
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string hashedPassword = Util.GetMd5Hash(model.Password);
            User myUser = db.Users.FirstOrDefault(u => u.Email == model.UserName && u.Password == hashedPassword);
            if (myUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifresi yanlış.");
                return View(model);
            }
            if (model.RememberMe)
            {
                HttpCookie myCookie = new HttpCookie("OESUserSettings");
                myCookie["Username"] = model.UserName;
                myCookie["Password"] = model.Password;
                myCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(myCookie);
            }
            else if (Request.Cookies["OESUserSettings"] != null)
            {
                HttpCookie myCookie = new HttpCookie("OESUserSettings");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            Util.UserInfo = new UserInfo
            {
                UserID = myUser.ID,
                NameSurname = myUser.Name + " " + myUser.Surname,
                IsAdmin = myUser.IsAdmin,
                Department = myUser.Department,
                StudentNumber = myUser.StudentNumber
            };
            string decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
                decodedUrl = Server.UrlDecode(returnUrl);
            if (Url.IsLocalUrl(decodedUrl))
            {
                return Redirect(decodedUrl);
            }
            if (myUser.IsAdmin)
            {
                ViewBag.IsAdmin = true;
                return RedirectToAction("index", "AdminHome");
            }
            ViewBag.IsAdmin = false;
            return RedirectToAction("index", "UserHome");
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("login", "login");
        }
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