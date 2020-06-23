using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumPost : ForumModel
    {
        public int ForumUserId { get; set; }
        public int ForumCategoryId { get; set; }
        public DateTime Date { get; set; }

        //public ForumUser ForumUser { get; set; }
        public ForumCategory ForumCategory { get; set; }
    }
}