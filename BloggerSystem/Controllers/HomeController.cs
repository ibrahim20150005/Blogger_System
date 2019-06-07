using BloggerSystem.CustomView;
using BloggerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BloggerSystem.Controllers
{
    public class HomeController : Controller
    {
        DbBloggerEntities2 db = new DbBloggerEntities2();
        // GET: Home
        public ActionResult Index()
        {


            CategoreyArticleModel cam = new CategoreyArticleModel
            {
                articleList = db.Articles.ToList(),
                categoryList = db.Categories.ToList()

            };


            return View(cam);
        }

        [HttpPost]
        public ActionResult Index(CategoreyArticleModel CategoryId)
        {
            CategoreyArticleModel cam = new CategoreyArticleModel();
            if (CategoryId.category.CategoryID != 0) { 
            

                     cam.articleList = db.Articles.ToList().Where(m => m.CategoryID == CategoryId.category.CategoryID);
                     cam.categoryList = db.Categories.ToList();
            } else{
                    cam.articleList = db.Articles.ToList();
                    cam.categoryList = db.Categories.ToList();
                }
                


            return View(cam);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentArticleModel cam = new CommentArticleModel
            {
                article = db.Articles.Find(id),
                commentList = db.Comments.ToList().Where(m => m.CommentID == id)
        };
            
            if (cam.article == null)
            {
                return HttpNotFound();
            }
            return View(cam);

        }
        [HttpPost]
        public ActionResult Details(CommentArticleModel cam) {

            cam.comment.UserID = 7;
            cam.comment.ArticleID = cam.article.ArticleID;
            db.Comments.Add(cam.comment);
            db.SaveChanges();
            return RedirectToAction("Details",cam.article.ArticleID);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}