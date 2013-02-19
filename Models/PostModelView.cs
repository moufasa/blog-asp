using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
    public class PostModelView
    {


        public string[] SelectedIds { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
        public Post currentPost { get; set; }

        public PostModelView(string[] selectedid, List<Tag> tags, Post post)
        {
            currentPost = post;
            SelectedIds = selectedid;
            Items = tags.Select(x => new SelectListItem
            {
                Value = x.TagId.ToString(),
                Text = x.Name
            });
        }
    }
}