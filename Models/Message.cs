using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Message
    {
        [ScaffoldColumn(false)]
        public int MessageId { get; set; }
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Votre nom est requis")]
        public string Name { get; set; }
        [DisplayName("Site")]
        [Required(ErrorMessage = "Laissez nous l'url de votre site")]
        public string Site { get; set; }
        [Required(ErrorMessage = "Un titre est requis")]
        [DisplayName("Titre")]
        public string Title { get; set; }
        [DisplayName("Contenu")]
        [Required(ErrorMessage = "Un contenu est requis")]
        [StringLength(160)]
        public string Content { get; set; }
        [ScaffoldColumn(false)]
        public bool isRead { get; set; }
        [ScaffoldColumn(false)]
        public bool isConfirm { get; set; }
    }
}