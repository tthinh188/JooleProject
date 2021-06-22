using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using DomainLayer;
using System.Web.Security;
using System.IO;


namespace JooleApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        // Validate User and Login
        [HttpPost]
        public ActionResult Login(tblUser user)
        {
            Service service = new Service();
            
            if (ModelState.IsValid)
            {
                var tuple = service.LoginUser(user.UserEmail, user.UserPassword);
                bool login = tuple.Item1;
                string image = tuple.Item2;
                if (login)
                {
                    FormsAuthentication.SetAuthCookie(user.UserEmail, true);
                    Session["userimage"] = image;
                    return RedirectToAction("SearchPage", "Search");
                }
                else
                {
                    ViewBag.LoginError = "Invalid Credentials";
                }
            }
            return View();
        }


        // Sign Up
        [HttpPost]
        public ActionResult AddUser(tblUser user, FormCollection fobj)
        {
            Service service = new Service();

            if (ModelState.IsValid)
            {
                bool unique = service.checkLoginID(user.UserName, user.UserEmail);
                if (unique)
                {
                    if (fobj["imghidden"] == "imgadded")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(user.UserImageFile.FileName);
                        string extension = Path.GetExtension(user.UserImageFile.FileName);
                        fileName = fileName + extension;
                        // fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        user.UserImage = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        user.UserImageFile.SaveAs(fileName);
                    }
                    else
                    {
                        user.UserImage = null;
                    }
                    service.insertUser(user);            
                }
                else
                {
                    ViewBag.UniqueError = "Duplicate Username Or Email";
                    ViewBag.ModalMessage = "ShowModal";
                }
            }
            else
            {
                ViewBag.ModalMessage = "ShowModal";
            }
            
            return View("Login");

        }



    }
}