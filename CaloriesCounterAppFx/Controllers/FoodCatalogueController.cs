using CaloriesCounterAppFx.Models;
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
        public ActionResult Index(string sortOrder, string searchString, string categoryIdFilter)
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

            foods = foods.Take(25);
            model.Foods = foods.ToList();
            var categories = db.FoodCategories.OrderBy(c => c.Name);
            model.Categories = categories.ToList();
            return View(model);
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