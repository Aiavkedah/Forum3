﻿using Forum.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class PostController : HomeController
    {
        [HttpGet]
        public ActionResult Posts(int? id, int page = 1)
        {
            if (id == null)
            {
                return RedirectToAction(CategoriesPage, "Home", Db.ForumCategories);
            }

            var category = Db.ForumCategories.Find(id);

            IEnumerable<ForumPost> posts = Db.ForumPosts.Where(i => i.ForumCategoryId == id)
                                                        .Include(i => i.ForumCategory)
                                                        .Include(i => i.ApplicationUser)
                                                        .OrderByDescending(i => i.Date);

            if (posts != null && category != null)
            {
                ViewBag.PostsCategory = category.Text;
                ViewBag.ForumCategoryId = id;
                ViewBag.User = User.Identity.GetUserId();

                if (page != 1)
                {
                    ViewBag.Count = PageSize - (PageSize * page - posts.Count());
                    ViewBag.Page = page - 1;
                }

                return View(posts.ToPagedList(page, PageSize));
            }

            return RedirectToAction(CategoriesPage, "Home", Db.ForumCategories);
        }


        [HttpPost]
        public ActionResult Posts(ForumPost post)
        {
            if (ModelState.IsValid)
            {
                post.ForumUserId = User.Identity.GetUserId();
                post.Date = DateTime.Now;

                Db.ForumPosts.Add(post);
                Db.SaveChanges();

                return RedirectToAction(CommentsPage, "Comment", new { id = post.ID });
            }

            return RedirectToAction(PostsPage, Db.ForumPosts);
        }
    }
}