using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingApp.Models;

namespace TestingApp.Controllers
{
    public class AuthenticationController : Controller
    {
        DbStudentEntities db = new DbStudentEntities();
     

        [HttpGet]
        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var findUser = db.Users.Where(x => x.Name.Equals(user.Name) && x.Password.Equals(user.Password)).FirstOrDefault();
            //var data = db.Users.Where(userData => userData.Name.Equals(user.Name) && userData.Password.Equals(user.Password)).ToList();

            if(findUser != null)
            {
                Session["Id"]   = findUser.Id;
                Session["Name"] = findUser.Name;
                Session["Role"] = findUser.Role;

                TempData["message-success"] = "Login successfully!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message-error"] = "Login Failed!";
                return RedirectToAction("Login", "Authentication");
            }


            //if(data.Count() > 0)
            //{
            //    Session["Id"] = data.FirstOrDefault().Id;
            //    Session["Name"] = data.FirstOrDefault().Name;
            //    Session["Password"] = data.FirstOrDefault().Password;
            //    Session["Role"] = data.FirstOrDefault().Role;

            //    TempData["message-success"] = "Login successfully!";
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    TempData["message-error"] = "Login Failed!";
            //    return RedirectToAction("Login", "Authentication");
            //}
        }

        [HttpGet]
        public ActionResult Logout() 
        {
            Session.Abandon();

            return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        public ActionResult ChangePassword(int id, User model) 
        {
            var user = db.Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
            user.Password = model.Password;
            db.SaveChanges();
            TempData["message-change-password"] = "Change password successfully !";

            return RedirectToAction("Login", "Authentication");
        }
    }
}