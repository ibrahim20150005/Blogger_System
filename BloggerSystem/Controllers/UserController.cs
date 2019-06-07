using BloggerSystem.CustomView;
using BloggerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerSystem.Controllers
{
    public class UserController : Controller
    {
        DbBloggerEntities2 db = new DbBloggerEntities2();
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid) {
                user.UserRoleID = 2;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var currentUser = db.Users.Where(m => m.Email == user.Email && m.Password == user.Password).SingleOrDefault();
            
            if (currentUser == null)
            {
                ViewBag.Message = "Username or password is wrong !";
            }
            else
            {
                Session["UserLoggedIn"] = currentUser;//object from user class save in session
                return RedirectToAction("contorolPanel");
            }

            return View();
        }


        public ActionResult contorolPanel()
        {
            var loggedUser = Session["UserLoggedIn"] as User; //custom session to user object
            if (loggedUser != null)
            {
                ViewBag.Name = loggedUser.Name;
                if (loggedUser.UserRoleID==1)
                {
                    //user is logged in
                    return View("~/Views/User/contorolPanel.cshtml",db.Articles.ToList());
                }
                else
                {
                    CategoreyArticleModel cam = new CategoreyArticleModel
                    {
                        articleList = db.Articles.ToList(),
                        categoryList = db.Categories.ToList()

                    };
                    return View("~/Views/Home/Index.cshtml",cam);
                }

            }
            else
            {
                //user is logged out
                return RedirectToAction("Login");
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Login");
        }

        public ActionResult AddCategory()
        {
       
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return View("~/Views/User/contorolPanel.cshtml", db.Articles.ToList());
        }

    }
}