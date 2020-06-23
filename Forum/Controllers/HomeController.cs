﻿using Forum.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        public ForumContext Db = new ForumContext();
        private string CategoriesPage = "Index";
        private string PostsPage = "Posts";
        private string CommentsPage = "Post";

        [HttpGet]
        public async Task<ActionResult> Index(int page=1)
        {
            int pageSize = 5;
            var categories = await Db.ForumCategories.ToListAsync();
            return View(categories.ToPagedList(page, pageSize));
        }

        [HttpPost]
        public ActionResult Index(ForumCategory category)
        {
            if (ModelState.IsValid)
            {
                Db.ForumCategories.Add(category);
                Db.SaveChanges();

                return RedirectToAction(PostsPage, new { id = category.ID });
            }
            //TODO: validation for empty value
            return RedirectToAction(CategoriesPage, Db.ForumCategories);
        }

        [HttpGet]
        public ActionResult Posts(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            IEnumerable<ForumPost> posts = Db.ForumPosts.Where(i => i.ForumCategoryId == id).Include(i => i.ForumCategory).OrderByDescending(i => i.Date);

            ViewBag.PostsCategory = posts.Count() > 0 ? posts.First().ForumCategory.Text : Db.ForumCategories.Where(i => i.ID == id).First().Text;
            ViewBag.ForumCategoryId = id;

            return View(posts);
        }

        [HttpPost]
        public ActionResult Posts(ForumPost post)
        {
            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                Db.ForumPosts.Add(post);
                Db.SaveChanges();
            }

            return RedirectToAction(CommentsPage, new { id = post.ID });
        }
        
        [HttpGet]
        public ActionResult Post(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            IEnumerable<ForumComment> comments = Db.ForumComments.Where(i => i.ForumPostId == id).Include(i => i.ForumPost).OrderByDescending(i => i.Date);

            var post = comments.Count() > 0 ? comments.First().ForumPost : Db.ForumPosts.Where(i => i.ID == id).First();
            ViewBag.PostTitle = post.Text;
            ViewBag.ForumCategoryId = post.ForumCategoryId;
            ViewBag.ForumPostId = post.ID;

            return View(comments);
        }

        [HttpPost]
        public ActionResult Post(ForumComment newComment)
        {
            if (ModelState.IsValid)
            {
                newComment.Date = DateTime.Now;
                Db.ForumComments.Add(newComment);
                Db.SaveChanges();
            }

            IEnumerable<ForumComment> comments = Db.ForumComments.Where(i => i.ForumPostId == newComment.ForumPostId).Include(i => i.ForumPost);
            ViewBag.PostId = newComment.ForumPostId;
            ViewBag.ForumCategoryId = comments.First().ForumPost.ForumCategoryId;

            return View(comments);
        }

        public ActionResult Delete(int id, string item)
        {
            object obj;

            Type type = Type.GetType(item);

            if (typeof(ForumCategory) == type)
            {
                obj = Db.ForumCategories.Find(id);
                ViewBag.Item = "Category";
            }
            else if (typeof(ForumPost) == type)
            {
                obj = Db.ForumPosts.Find(id);
                ViewBag.Item = "Post";
            }
            else if (typeof(ForumComment) == type)
            {
                obj = Db.ForumComments.Find(id);
                ViewBag.Item = "Comment";
            }
            else
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            /* foreach (PropertyInfo prop in type.GetProperties().Where(i => i.Name == "ID" && Convert.ToInt32(i.GetValue(obj)) == id))
             {

             }*/
            
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string item)
        {
            string page = default;
            int itemId = 0;

            Type type = Type.GetType(item);

            if (typeof(ForumCategory) == type)
            {
                Db.ForumCategories.Remove(Db.ForumCategories.Find(id));
                page = CategoriesPage;
            }
            else if (typeof(ForumPost) == type)
            {
                var post = Db.ForumPosts.Find(id);
                itemId = post.ForumCategoryId;
                Db.ForumPosts.Remove(post);
                page = PostsPage;
            }
            else if (typeof(ForumComment) == type)
            {
                var comment = Db.ForumComments.Find(id);
                itemId = comment.ForumPostId;
                Db.ForumComments.Remove(comment);
                page = CommentsPage;
            }
            
            Db.SaveChanges();
            
            return RedirectToAction(page, new { id = itemId });
        }


        public ActionResult Edit(int? id, string item, int page=1)
        {
            if (item == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            object obj;

            Type type = Type.GetType(item);

            if (typeof(ForumCategory) == type)
            {
                obj = Db.ForumCategories.Find(id);
                ViewBag.Item = "Category";
            }
            else if (typeof(ForumPost) == type)
            {
                obj = Db.ForumPosts.Find(id);
                ViewBag.Item = "Post";
            }
            else if (typeof(ForumComment) == type)
            {
                obj = Db.ForumComments.Find(id);
                ViewBag.Item = "Comment";
            }
            else
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            return View("Edit", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ForumModel model, string item)
        {
            object obj;
            string page = default;
            int itemId = 0;
            Type type = Type.GetType(item);

            if (ModelState.IsValid)
            {
                if (typeof(ForumCategory) == type)
                {
                    ForumCategory category = Db.ForumCategories.Find(model.ID);
                    category.Text = model.Text;
                    obj = category;
                    page = CategoriesPage;
                }
                else if (typeof(ForumPost) == type)
                {
                    ForumPost post = Db.ForumPosts.Find(model.ID);
                    post.Text = model.Text;
                    obj = post;
                    itemId = post.ForumCategoryId;
                    page = PostsPage;
                }
                else if (typeof(ForumComment) == type)
                {
                    ForumComment comment = Db.ForumComments.Find(model.ID);
                    comment.Text = model.Text;
                    obj = comment;
                    itemId = comment.ForumPostId;
                    page = CommentsPage;
                }
                else
                {
                    obj = default;
                }

            
                Db.Entry(obj).State = EntityState.Modified;
                Db.SaveChanges();
              
                return RedirectToAction(page, new { id = itemId });
            }

            return View("Edit", model);
        }
    }
}