using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ForumPost : ForumModel
    {
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public override string Text { get; set; }
        [Display(Name = "User Name")]
        public string ForumUserId { get; set; }
        public int ForumCategoryId { get; set; }
        public DateTime Date { get; set; }

        public ForumCategory ForumCategory { get; set; }
        [ForeignKey("ForumUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}