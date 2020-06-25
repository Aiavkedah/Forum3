using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ForumPost : ForumModel
    {
        [Display(Name = "User Name")]
        public string ForumUserId { get; set; }
        public int ForumCategoryId { get; set; }
        public DateTime Date { get; set; }

        public ForumCategory ForumCategory { get; set; }
        [ForeignKey("ForumUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}