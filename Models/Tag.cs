using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        
        public string Name { get; set; }

        public virtual List<Post> Posts {get; set;}
    }
}