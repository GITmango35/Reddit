using System;
using System.Collections.Generic;

#nullable disable

namespace PROJET_FIN.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? UpVote { get; set; }
        public int? DownVote { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
