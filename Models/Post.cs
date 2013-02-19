using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Post
    {
        [ScaffoldColumn(false)]
        public int PostId { get; set; }
        
        [Required(ErrorMessage = "Le nom est un champ obligatoire")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Le titre est un champ obligatoire")]
        [StringLength(160)]
        public string Title { get; set; }
        
        [StringLength(50)]
        public string Slug { get; set; }
        
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Contenu est un champ obligatoire")]
        [AllowHtml]
        public string Content { get; set; }

        //public int[] KeysId { get; set; }

        public virtual List<Tag> Tags { get; set; }
        

        [DisplayName("Categorie")]
        public Category Categorie { get; set; }
        public int CategoryId { get; set; }

        public bool Online { get; set; }
    }
}
