using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumPost
    {
        public int ID { get; set; }
        public int ForumUserId { get; set; }
        public int ForumCategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        //public ForumUser ForumUser { get; set; }
        public ForumCategory ForumCategory { get; set; }
    }
}