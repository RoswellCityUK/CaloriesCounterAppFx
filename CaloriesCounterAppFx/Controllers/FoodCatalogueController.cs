using CaloriesCounterAppFx.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
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
            //Decoding HTML and other special chars like < and > to &lt; and &gt; to protect script from
            //harmful code execution from User input
            sortOrder = HttpUtility.HtmlEncode(sortOrder);
            searchString = HttpUtility.HtmlEncode(searchString);
            categoryIdFilter = HttpUtility.HtmlEncode(categoryIdFilter);

            //Creating model extending BaseViewModel
            var model = base.CreateModel<FoodCatalogueIndexViewModel>(); 
            
            //Defining beginning of the LINQ query to include records from Food, Nutrients and Category tables
            var foods = db.Foods.Include(f => f.Nutrients).Include(f => f.Category);

            //Setting default sorting if Sort param empty 
            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "name_asc";
            }

            //Passing current received Sort Order param to View
            ViewBag.SortParam = sortOrder;
            //Constructing further part of LINQ query in regard of passed parameter
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

            //Filtering records with Search Param if not null nor empty
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchParam = searchString;
                foods = foods.Where(f => f.Name.Contains(searchString));
            }

            //Filtering records by category filter if parameter not null nor empty
            if (!String.IsNullOrEmpty(categoryIdFilter))
            {
                int categoryId = int.Parse(categoryIdFilter);
                //Passing current category filter parameter to View
                ViewBag.CategoryId = categoryId;
                foods = foods.Where(f => f.Category.Id == categoryId);
            }

            //Defining variables needed for pagination of records
            int numberOfRecords = foods.Count();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            int numberOfPages = numberOfRecords / pageSize;
            if((numberOfRecords % pageSize) != 0)
            {
                numberOfPages++;
            }
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageNumber > numberOfPages)
                pageNumber = numberOfPages;
            int skipRows = (pageNumber - 1) * pageSize;

            //Pasing pagination variables to View
            ViewBag.NumberOfRecords = numberOfRecords;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.NumberOfPages = numberOfPages;
            ViewBag.SkipRows = skipRows;

            //Skipping the appropriate number of records
            if (numberOfRecords > 0)
            {
                foods = foods.Skip(skipRows).Take(pageSize);
                model.Foods = foods.ToList();
            }
            else
            {
                model.Foods = new List<Food>();
                ViewBag.SipRows = 0;
            }

            //Getting and passing Categories list for filter dropdown
            var categories = db.FoodCategories.OrderBy(c => c.Name);
            model.Categories = categories.ToList();

            return View(model);
        }

        //Adding food record to diary from Add Food Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToDiary(AddToDiaryViewModel addToDiary)
        {
            //Getting current User and chosen Food entities from DbContext
            string currentUserId = this.User.Identity.GetUserId();
            ApplicationUser CurrentUser = db.Users.Include( u => u.ConsumedCalories).Where( u => u.Id.Equals(currentUserId)).FirstOrDefault();
            Food food = db.Foods.Where( f => f.Id.Equals(addToDiary.FoodId)).FirstOrDefault();

            //Building model for consumed food record
            ConsumedCalories consumedCalories = new ConsumedCalories();
            consumedCalories.Food = food;
            consumedCalories.Amount = addToDiary.PortionAmount;
            consumedCalories.DateAdded = DateTime.Now;

            //Handling errors and adding to the database
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