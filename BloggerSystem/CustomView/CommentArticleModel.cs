using BloggerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerSystem.CustomView
{
    public class CommentArticleModel
    {
        public Article article { get; set; }

        public Comment comment { get; set; }

        public IEnumerable<Comment> commentList { get; set; }
    }
}