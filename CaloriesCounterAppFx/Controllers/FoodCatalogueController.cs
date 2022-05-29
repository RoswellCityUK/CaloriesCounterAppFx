using CaloriesCounterAppFx.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Controllers
{
    public class FoodCatalogueController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: FoodCatalogue
        public ActionResult Index(string sortOrder, string searchString, string categoryIdFilter, int? page)
        {
            var model = base.CreateModel<FoodCatalogueIndexViewModel>(); 
            var foods = db.Foods.Include(f => f.Nutrients).Include(f => f.Category);

            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "name_asc";
            }

            ViewBag.SortParam = sortOrder;
            switch (sortOrder)
            {
                case "name_desc":
                    foods = foods.OrderByDescending(f => f.Name);
                    break;
                case "cat_asc":
                    foods = foods.OrderBy(f => f.Category.Name);
                    break;
                case "cat_desc":
                    foods = foods.OrderByDescending(f => f.Category.Name);
                    break;
                case "energy_asc":
                    foods = foods.OrderBy(f => f.Nutrients.FirstOrDefault().EnergKcal);
                    break;
                case "energy_desc":
                    foods = foods.OrderByDescending(f => f.Nutrients.FirstOrDefault().EnergKcal);
                    break;
                default:
                    foods = foods.OrderBy(f => f.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchParam = searchString;
                foods = foods.Where(f => f.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(categoryIdFilter))
            {
                int categoryId = int.Parse(categoryIdFilter);
                ViewBag.CategoryId = categoryId;
                foods = foods.Where(f => f.Category.Id == categoryId);
            }

            int numberOfRecords = foods.Count();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            int numberOfPages = numberOfRecords / pageSize;
            if((numberOfRecords % pageSize) != 0)
            {
                numberOfPages = numberOfPages + 1;
            }
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageNumber > numberOfPages)
                pageNumber = numberOfPages;
            int skipRows = (pageNumber - 1) * pageSize;

            ViewBag.NumberOfRecords = numberOfRecords;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.NumberOfPages = numberOfPages;
            ViewBag.SkipRows = skipRows;

            foods = foods.Skip(skipRows).Take(pageSize);
            model.Foods = foods.ToList();
            var categories = db.FoodCategories.OrderBy(c => c.Name);
            model.Categories = categories.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToDiary(AddToDiaryViewModel addToDiary)
        {
            string currentUserId = this.User.Identity.GetUserId();
            ApplicationUser CurrentUser = db.Users.Include( u => u.ConsumedCalories).Where( u => u.Id.Equals(currentUserId)).FirstOrDefault();
            Food food = db.Foods.Where( f => f.Id.Equals(addToDiary.FoodId)).FirstOrDefault();
            ConsumedCalories consumedCalories = new ConsumedCalories();
            consumedCalories.Food = food;
            consumedCalories.Amount = addToDiary.PortionAmount;
            consumedCalories.DateAdded = DateTime.Now;

            if (ModelState.IsValid)
            {
                CurrentUser.ConsumedCalories.Add(consumedCalories);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Food Added to Your Diary successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMsg"] = "";

                foreach (var items in ModelState.Values)
                {
                    foreach (var er in items.Errors)
                    {

                        TempData["ErrorMsg"] = TempData["ErrorMsg"].ToString() + " " + er.ErrorMessage.ToString();
                    }

                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}