using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumModel
    {
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
    }
}