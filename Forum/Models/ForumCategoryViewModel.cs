using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Models
{
    public class ForumCategoryViewModel
    {
        public PagedList.IPagedList<ForumCategory> Categories { get; set; }
        public IEnumerable<ForumPost> Posts { get; set; }
    }
}