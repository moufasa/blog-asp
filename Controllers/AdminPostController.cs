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
    [Authorize]
    public class AdminPostController : Controller
    {

        public BlogContext db = new BlogContext();

        public static string prepareSlug(string str)
        {
            string preparedString = str.ToLower().Replace(" ", "-");
            preparedString = Regex.Replace(preparedString, @"[^a-zA-Z0-9\/_|+ -]", "");
            return preparedString;
        }

        //
        // GET: /AdminPost/

        public ViewResult Index()
        {
            IEnumerable<Post> posts;
            if (HttpContext.User.IsInRole("Administrator"))
            {
                posts = db.Posts
                    .Include(p => p.Categorie);
            }
            else {
                posts = db.Posts
                        .Where(p => p.Author == HttpContext.User.Identity.Name)
                        .Include(p => p.Categorie);
            }
            return View(posts.ToList());
        }

        //
        // GET: /AdminPost/Details/5

        public ViewResult Details(int id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }

        //
        // GET: /AdminPost/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            var model = new PostModelView(new String[0], db.Tags.ToList(), new Post());

            return View(model);
        } 

        //
        // POST: /AdminPost/Create

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "currentPost")] Post post, int CategoryId, int[] SelectedIds)
        {
            if (!HttpContext.User.IsInRole("Administrator"))
            {
                post.Author = HttpContext.User.Identity.Name;
            }

            post.Categorie = db.Categories.Single(p => p.CategoryId == CategoryId);
            post.Slug = (post.Slug != null && post.Slug != "") ? prepareSlug(post.Slug) : prepareSlug(post.Title);

            // on peuple les keys.
            foreach (var keyId in SelectedIds)
            {
                Tag key = db.Tags.Single(p => p.TagId == keyId);
                key.Posts.Add(post);
            }

            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //
        // GET: /AdminPost/Edit/5
 
        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Find(id);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            String[] populatekey = new String[post.Tags.Count];
            int cpt = 0;
            foreach (Tag key in post.Tags)
            {
                populatekey[cpt] = key.TagId.ToString();
                cpt++;
            }
            var model = new PostModelView(populatekey, db.Tags.ToList(), post);
            return View(model);
        }

        //
        // POST: /AdminPost/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "currentPost")] Post post, int CategoryId, int[] SelectedIds)
        {

            if (!HttpContext.User.IsInRole("Administrator"))
            {
                post.Author = HttpContext.User.Identity.Name;
            }

            post.Slug = (post.Slug != null && post.Slug != "") ? prepareSlug(post.Slug) : prepareSlug(post.Title);

            var manager = db.Entry<Post>(post);
            // on préviens que ça va changer
            manager.State = EntityState.Modified;
            //on force le chargement des relations
            manager.Collection(k => k.Tags).Load();
            //on vide ce qu'il y avait en base
            post.Tags.Clear();
            // on peuple les keys.
            foreach (var tagId in SelectedIds)
            {
                Tag key = db.Tags.Single(p => p.TagId == tagId);
                post.Tags.Add(key);
            }
            post.CategoryId = CategoryId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /AdminPost/Delete/5
 
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }

        // POST: /AdminPost/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}