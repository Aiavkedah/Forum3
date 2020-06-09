using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumComment
    {
        public int ID { get; set; }
        public int ForumUserId { get; set; }
        public int ForumPostId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public ForumPost ForumPost { get; set; }
    }
}