using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;




#nullable disable

namespace PROJET_FIN.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "SVP entrez votre nom d'utilisateur")]
        //[MaxLength(20, ErrorMessage="Too long")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "SVP entrez votre courriel")]
        [RegularExpression(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Utilisez un courriel valide")]
        public string Email { get; set; }
        [Required(ErrorMessage = "SVP entrez votre mot de passe")]
        public string Password { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
