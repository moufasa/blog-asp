using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;


namespace Blog.Controllers
{
    public class PageController : Controller
    {

        BlogContext db = new BlogContext();

        //
        // GET: /Page/

        public ActionResult GetMenuPages()
        {
            var pages = db.Pages.Where(p => p.Online);
            return View(pages);
        }



        //
        // GET: /Store/Details
        public ActionResult Details(int id, string title)
        {
            var page = db.Pages.Find(id);
            return View(page);
        }

    }
}
