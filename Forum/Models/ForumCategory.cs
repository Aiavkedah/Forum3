using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumCategory : ForumModel
    {
        [Display(Name = "Category")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 1)]
        public override string Text { get; set; }
    }
}