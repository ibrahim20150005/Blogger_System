using BloggerSystem.CustomView;
using BloggerSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BloggerSystem.Controllers
{
    public class ArticleController : Controller
    {
        DbBloggerEntities2 db = new DbBloggerEntities2();

      
        // GET: Article/Create
        public ActionResult Create()
        {
            var CategeoryType = db.Categories.ToList();

            CategoreyArticleModel cam = new CategoreyArticleModel
            {
                categoryList = CategeoryType
            };

            return View(cam);
        }

        // POST: Article/Create
        [HttpPost]
        public ActionResult Create(CategoreyArticleModel cam)
        {
            if (ModelState.IsValid) {
                cam.article.UserID = 5;
                db.Articles.Add(cam.article);
                db.SaveChanges();
                // TODO: Add insert logic here

                return View("~/Views/User/contorolPanel.cshtml", db.Articles.ToList());

            }
                return View("Create");
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int id)
        {
            var CategeoryType = db.Categories.ToList();

            CategoreyArticleModel cam = new CategoreyArticleModel
            {
                categoryList = CategeoryType,
                article = db.Articles.SingleOrDefault(m=>m.ArticleID==id)
            };

            return View(cam);
        }

        // POST: Article/Edit/5
        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return View("~/Views/User/contorolPanel.cshtml", db.Articles.ToList());
            }
            return View(article);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return View("~/Views/User/contorolPanel.cshtml", db.Articles.ToList());
        }

        // GET: Article/Details/6
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
    }
}
