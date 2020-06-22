using Forum.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        public ForumContext Db = new ForumContext();

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
            }
            
            return RedirectToAction("Posts", new { id = category.ID });
        }

        [HttpGet]
        public ActionResult Posts(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", Db.ForumCategories);
            }

            IEnumerable<ForumPost> posts = Db.ForumPosts.Where(i => i.ForumCategoryId == id).Include(i => i.ForumCategory);

            ViewBag.PostsCategory = posts.Count() > 0 ? posts.First().ForumCategory.Name : Db.ForumCategories.Where(i => i.ID == id).First().Name;
            ViewBag.ForumCategoryId = id;

            return View(posts);
        }

        [HttpPost]
        public ActionResult Posts(ForumPost post)
        {
            if (ModelState.IsValid)
            {
                Db.ForumPosts.Add(post);
                Db.SaveChanges();
            }

            return RedirectToAction("Post", new { id = post.ID });
        }
        
        [HttpGet]
        public ActionResult Post(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", Db.ForumCategories);
            }

            IEnumerable<ForumComment> comments = Db.ForumComments.Where(i => i.ForumPostId == id).Include(i => i.ForumPost);

            var post = comments.Count() > 0 ? comments.First().ForumPost : Db.ForumPosts.Where(i => i.ID == id).First();
            ViewBag.PostTitle = post.Title;
            ViewBag.ForumCategoryId = post.ForumCategoryId;
            ViewBag.ForumPostId = id;

            return View(comments);
        }

        [HttpPost]
        public ActionResult Post(ForumComment newComment)
        {
            newComment.Date = DateTime.Now;

            Db.ForumComments.Add(newComment);
            Db.SaveChanges();

            IEnumerable<ForumComment> comments = Db.ForumComments.Where(i => i.ForumPostId == newComment.ForumPostId);
            //ViewBag.ForumComment = comments;
            ViewBag.PostId = newComment.ForumPostId;
            return View(comments);
        }

        public ActionResult Delete(int id)
        {
            ForumCategory category = Db.ForumCategories.Find(id);

            if (category == null)
            {
                return RedirectToAction("Index", Db.ForumCategories);
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumCategory category = Db.ForumCategories.Find(id);

            if (category != null)
            {
                Db.ForumCategories.Remove(category);
                Db.SaveChanges();
            }

            return RedirectToAction("Index", Db.ForumCategories);
        }


        public ActionResult Edit(int? id, int page=1)
        {
            if (id != null)
            {
                ForumCategory category = Db.ForumCategories.Find(id);

                if (category != null)
                {
                    return PartialView("Edit", category);
                }
            }
            return RedirectToAction("Index", Db.ForumCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ForumCategory category, int page)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(category).State = EntityState.Modified;
                Db.SaveChanges();
              
                return RedirectToAction("Index", new { page = page });
            }

            return PartialView("Edit", category);
        }
    }
}