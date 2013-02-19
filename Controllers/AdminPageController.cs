using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    [Authorize]
    public class AdminPageController : Controller
    {
        private BlogContext db = new BlogContext();
        //
        // GET: /AdminPage/

        public ViewResult Index()
        {
            var pages = db.Pages;
            return View(pages.ToList());
        }

        //
        // GET: /AdminPage/Details/5

        public ViewResult Details(int id)
        {
            Page page = db.Pages.Find(id);
            return View(page);
        }

        //
        // GET: /AdminPage/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AdminPage/Create

        [HttpPost,ValidateInput(false)]
        public ActionResult Create(Page page)
        {
            if (ModelState.IsValid)
            {
                db.Pages.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(page);
        }
        
        //
        // GET: /AdminPage/Edit/5
 
        public ActionResult Edit(int id)
        {
            Page page = db.Pages.Find(id);
            return View(page);
        }

        //
        // POST: /AdminPage/Edit/5

        [HttpPost,ValidateInput(false)]
        public ActionResult Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page);
        }

        //
        // GET: /AdminPage/Delete/5
 
        public ActionResult Delete(int id)
        {
            Page page = db.Pages.Find(id);
            return View(page);
        }

        //
        // POST: /AdminPage/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
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