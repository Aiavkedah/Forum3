using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumComment : ForumModel
    {
        [Display(Name = "User Name")]
        public string ForumUserId { get; set; }
        public int ForumPostId { get; set; }
        public DateTime Date { get; set; }

        public ForumPost ForumPost { get; set; }
        [ForeignKey("ForumUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}