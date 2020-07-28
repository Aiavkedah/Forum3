using Forum.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext Db = new ApplicationDbContext();
        protected readonly string CategoriesPage = "Index";
        protected readonly string PostsPage = "Posts";
        protected readonly string CommentsPage = "Comments";
        protected readonly int PageSize = 5;

        [HttpGet]
        public ActionResult Index(int page=1)
        {
            ForumCategoryViewModel view = new ForumCategoryViewModel
            {
                Categories = Db.ForumCategories.ToList().ToPagedList(page, PageSize),
                Posts = Db.ForumPosts.ToList()
            };
            
            ViewBag.Count = PageSize - (PageSize * page - Db.ForumCategories.Count());
            ViewBag.Page = page - 1;

            return View(view);
        }

        [HttpPost]
        public ActionResult Index(ForumCategory category)
        {
            if (ModelState.IsValid)
            {
                Db.ForumCategories.Add(category);
                Db.SaveChanges();

                return RedirectToAction(PostsPage, "Post", new { id = category.ID });
            }
           
            return RedirectToAction(CategoriesPage, Db.ForumCategories);
        }

        public ActionResult Delete(int id, string type)
        {
            var item = FindItemById(id, type);

            if (item == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string type, string returnURL)
        {
            var item = FindItemById(id, type);

            if (item == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            Db.Entry(item).State = EntityState.Deleted;
            Db.SaveChanges();

            return Redirect(returnURL);
        }

        [HttpGet]
        public ActionResult Edit(int id, string type)
        {
            var item = FindItemById(id, type);

            if (item == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ForumModel model, string type, string returnURL)
        {
            var item = FindItemById(model.ID, type);

            if (item == null)
            {
                return RedirectToAction(CategoriesPage, Db.ForumCategories);
            }

            if (ModelState.IsValid)
            {
                ((ForumModel)item).Text = model.Text;

                try
                {
                    Db.Entry(item).State = EntityState.Modified;
                    Db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessage = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                    ViewBag.ErrorMessage = string.Join("; ", errorMessage);
                    return View(model);
                }

                return Redirect(returnURL);
            }

            return View(model);
        }

        public object FindItemById(int id, string type)
        {
            Type itemType = Type.GetType(type);

            Type tableType = typeof(DbSet<>).MakeGenericType(itemType);

            PropertyInfo propertyInfo = Db.GetType().GetProperties().Where(p => p.PropertyType == tableType).FirstOrDefault();

            return Db.Set(itemType).Find(id);
        }

    }
}