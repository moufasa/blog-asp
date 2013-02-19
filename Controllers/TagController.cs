using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{ 
    public class TagController : Controller
    {
        private BlogContext db = new BlogContext();

        //
        // GET: /Tag/

        public ViewResult getPosts(int tagId, string name)
        {
            Tag tag = db.Tags.Single(t => t.TagId == tagId);
            ViewBag.currentTag = tag.Name;
            return View(tag.Posts.ToList());
        }
    }
}