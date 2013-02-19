using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class PostController : Controller
    {


        BlogContext db = new BlogContext();

        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult getWithLimit(int limit)
        {
            var posts = db.Posts
                .Where(p => p.Online)
                .OrderByDescending(p => p.Date)
                .Take(limit)
                .Include(p => p.Categorie)
                .Include(p => p.Tags);
  
            return View(posts.ToList());
        }

        public ActionResult Navigation(int category, string title)
        {
            var posts = db.Posts
                .Where(p => p.Categorie.CategoryId == category)
                .OrderByDescending(p => p.Date)
                .Include(p => p.Categorie)
                .Include(p => p.Tags);

            ViewBag.CurrentCategory = db.Categories.Find(category).Name;
            return View(posts);
        }

        
        public ActionResult Details(int id, string title)
        {
            var post = db.Posts.Find(id);
            ViewBag.categorie = db.Categories.Find(post.CategoryId);
            return View(post);
        }

        public ActionResult GetMenuCategories()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }


        public ActionResult Archive()
        {
            var posts = db.Posts.ToList();
            return View(posts);
        }

        public ActionResult GetArchives(string month, string year)
        {
            var refDateMin = new DateTime(int.Parse(year), int.Parse(month), 1, 0, 0, 0);
            var refDateMax = refDateMin.AddMonths(1);

            var posts = db.Posts
                .Where(p => p.Date >= refDateMin)
                .Where(p => p.Date <= refDateMax)
                .Include(p => p.Categorie)
                .Include(t => t.Tags);

            ViewBag.Month = month;
            ViewBag.Year = year;
            return View(posts);
        }

    }
}
