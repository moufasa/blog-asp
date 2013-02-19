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
    public class MessageController : Controller
    {
        public BlogContext db = new BlogContext();

        //
        // GET: /Message/

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /PostManager/Create

        [HttpPost]
        public ActionResult Index(Message message)
        {
            message.isConfirm = false;
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Success", new { controller = "Message", action = "Success", id = message.MessageId });
            }
            
            return View(message);
        }

        public ActionResult Success(int id)
        {
            var message = db.Messages.Find(id);
            return View(message);
        }

        public ActionResult confirm(int id, bool confirm)
        {
            var message = db.Messages.Find(id);
            message.isConfirm = confirm;
            db.Entry(message).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { controller = "Home", action = "Index" });
        }

    }
}
