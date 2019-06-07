using BloggerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerSystem.CustomView
{
    public class CategoreyArticleModel
    {
        public Article article { get; set; }

        public Category category { get; set; }

        public IEnumerable<Article> articleList { get; set; }

        public IEnumerable<Category> categoryList { get; set; }

    }
}