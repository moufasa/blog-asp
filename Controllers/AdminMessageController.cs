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
    public class AdminMessageController : Controller
    {
        private BlogContext db = new BlogContext();

        //
        // GET: /AdminMessage/

        public ViewResult Index()
        {
            return View(db.Messages.Where(m => m.isConfirm == true).OrderByDescending(m => m.MessageId).ToList());
        }

        public void Update(int id)
        { 
            var msg = db.Messages.Find(id);
            if (TryUpdateModel(msg))
            {
                msg.isRead = !msg.isRead;
                db.SaveChanges();
            }
        }

        public int CounterMessages(string statment)
        {
            return db.Messages.Where(m => m.isConfirm == true).Count();     
        }

        //
        // GET: /AdminMessage/Details/5

        public ViewResult Details(int id)
        {
            Message message = db.Messages.Find(id);
            var msg = db.Messages.Find(id);
            if (TryUpdateModel(msg))
            {
                msg.isRead = true;
            }    
            db.SaveChanges();
            return View(message);
        }
        

        //
        // GET: /AdminMessage/Delete/5
 
        public ActionResult Delete(int id)
        {
            Message message = db.Messages.Find(id);
            return View(message);
        }

        //
        // POST: /AdminMessage/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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