using CaloriesCounterAppFx.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Controllers
{
    public class MyDiaryController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MyDiary
        public ActionResult Index(string dateDayParam, string dateMonthParam, string dateYearParam)
        {
            //Main My Diary controller
            //Tomasz Grabowski 22/05/2022

            //Creating model based on BaseViewModel that includes LoginPartialViewModel
            MyDiaryViewModel model = base.CreateModel<MyDiaryViewModel>();

            int dateDay;
            int dateMonth;
            int dateYear;
            DateTime date;
            if (int.TryParse(dateDayParam, out dateDay) && int.TryParse(dateMonthParam, out dateMonth) && int.TryParse(dateYearParam, out dateYear))
            {
                date = new DateTime(dateYear, dateMonth, dateDay);
            }
            else
            {
                date = DateTime.Today;
            }
            model.Date = date;

            //Getting current user Id
            string currentUserId = User.Identity.GetUserId();
            model.User = db.Users.Include(u => u.ConsumedCalories).Where(u => u.Id == currentUserId).FirstOrDefault(); ;

            //contructing LINQ query for Consumed Calories table from User related entity
            model.ConsumedCaloriesOnDay = model.User.ConsumedCalories.AsQueryable().Where( c => c.DateAdded.Date == date.Date).ToList();

            model.EnergyOnDay = 0;
            model.ProteinOnDay = 0;
            model.FatOnDay = 0;
            model.CarbsOnDay = 0;
            model.WeightOnDay = 0;
            
            //Calculating all the nutrition included in consumed food items
            foreach (ConsumedCalories calories in model.ConsumedCaloriesOnDay)
            {
                var temp = calories.Food.Nutrients.FirstOrDefault();
                model.EnergyOnDay += (temp.EnergKcal/100) * calories.Amount;
                model.ProteinOnDay += (temp.ProteinG / 100) * calories.Amount;
                model.FatOnDay += (temp.LipidTotG / 100) * calories.Amount;
                model.CarbsOnDay += (temp.CarbohydrtG / 100) * calories.Amount;
                model.WaterOnDay += (temp.WaterG / 100) * calories.Amount;
                model.WeightOnDay += calories.Amount;
            }

            //LINQ query to retrieve all the calories history data
            model.ConsumedCaloriesHistory = model.User.ConsumedCalories.AsQueryable()
                //Ordering by date for more intuitive layout
                .OrderByDescending(c => c.DateAdded).GroupBy(c => c.DateAdded.Date)
                //Selecting data from Database in a way that give us ConsumedCaloriesHistoryViewModel
                .Select(x => new ConsumedCaloriesHistoryViewModel { Date = x.Key, EnergySumOnDate = x.Sum(e => e.Food.Nutrients.FirstOrDefault().EnergKcal / 100 * e.Amount) })
                .ToList();

            return View(model);
        }

        //Method for reseting Calories Counter
        //Tomasz Grabowski 22/05/2022
        public ActionResult Reset() {

            //Getting current user Id
            string currentUserId = User.Identity.GetUserId();

            //Retrieving current User entity for DbContext including associated ConsumedCalories entities
            var currentUser = db.Users.Include(u => u.ConsumedCalories).Where(u => u.Id == currentUserId).FirstOrDefault();
            //Filtering and retrieving ConsumedCalories for a given date
            var caloriesToDelete = currentUser.ConsumedCalories.Where(c => c.DateAdded.Date == DateTime.Today.Date).ToList();
            
            //For some reason DbContext.ConsumedCalories.RemoveRange(caloriesToDelete) didn't gave assumed result
            //so I had to make a small walk-around and attach every retreived entity to DbContext and then remove one-by-one
            foreach(ConsumedCalories calories in caloriesToDelete)
            {
                var caloriesTemp = db.ConsumedCaloriesTable.Attach(calories);
                db.ConsumedCaloriesTable.Remove(caloriesTemp);
            }
            db.SaveChanges();


            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}