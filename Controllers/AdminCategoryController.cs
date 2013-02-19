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
    public class AdminCategoryController : Controller
    {
        private BlogContext db = new BlogContext();

        //
        // GET: /AdminTag/

        public ViewResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /AdminTag/Details/5

        public ViewResult Details(int id)
        {
            Category tag = db.Categories.Find(id);
            return View(tag);
        }

        //
        // GET: /AdminTag/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AdminTag/Create

        [HttpPost]
        public ActionResult Create(Category tag)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tag);
        }
        
        //
        // GET: /AdminTag/Edit/5
 
        public ActionResult Edit(int id)
        {
            Category tag = db.Categories.Find(id);
            return View(tag);
        }

        //
        // POST: /AdminTag/Edit/5

        [HttpPost]
        public ActionResult Edit(Category tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        //
        // GET: /AdminTag/Delete/5
 
        public ActionResult Delete(int id)
        {
            Category tag = db.Categories.Find(id);
            return View(tag);
        }

        //
        // POST: /AdminTag/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Category tag = db.Categories.Find(id);
            db.Categories.Remove(tag);
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