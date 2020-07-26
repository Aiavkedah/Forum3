using Forum.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class CommentController : HomeController
    {
        [HttpGet]
        public ActionResult Comments(int? id, int page = 1)
        {
            if (id == null)
            {
                return RedirectToAction(CategoriesPage, "Home", Db.ForumCategories);
            }

            var post = Db.ForumPosts.Find(id);

            IEnumerable<ForumComment> comments = Db.ForumComments.Where(i => i.ForumPostId == id).Include(i => i.ForumPost).Include(i => i.ApplicationUser).OrderByDescending(i => i.Date);

            if (comments != null && post != null)
            {
                ViewBag.PostTitle = post.Text;
                ViewBag.ForumCategoryId = post.ForumCategoryId;
                ViewBag.ForumPostId = post.ID;
                ViewBag.User = User.Identity.GetUserId();

                return View(comments.ToPagedList(page, PageSize));
            }

            return RedirectToAction(CategoriesPage, "Home", Db.ForumCategories);
        }

        [HttpPost]
        public ActionResult Comments(ForumComment newComment)
        {
            if (ModelState.IsValid)
            {
                newComment.Date = DateTime.Now;

                Db.ForumComments.Add(newComment);
                Db.SaveChanges();
            }

            IEnumerable<ForumComment> comments = Db.ForumComments.Where(i => i.ForumPostId == newComment.ForumPostId).Include(i => i.ForumPost).Include(i => i.ApplicationUser).OrderByDescending(i => i.Date);

            if (comments != null)
            {
                ViewBag.PostId = newComment.ForumPostId;
                ViewBag.ForumCategoryId = comments.First().ForumPost.ForumCategoryId;
                ViewBag.User = User.Identity.GetUserId();

                return View(comments.ToPagedList(1, PageSize));
            }

            return RedirectToAction(CategoriesPage, "Home", Db.ForumCategories);
        }
    }
}