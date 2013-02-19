using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Blog.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }

        public BlogContext()
            : base("DefaultConnection")
        {
        }
 
          internal void SaveSchanges()
          {
              throw new NotImplementedException();
          }

    }
}