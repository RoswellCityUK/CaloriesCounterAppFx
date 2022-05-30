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

            string currentUserId = User.Identity.GetUserId();
            model.User = db.Users.Include(u => u.ConsumedCalories).Where(u => u.Id == currentUserId).FirstOrDefault(); ;

            model.ConsumedCaloriesOnDay = model.User.ConsumedCalories.AsQueryable().Where( c => c.DateAdded.Date == date.Date).ToList();

            model.EnergyOnDay = 0;
            model.ProteinOnDay = 0;
            model.FatOnDay = 0;
            model.CarbsOnDay = 0;
            model.WeightOnDay = 0;
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

            model.ConsumedCaloriesHistory = model.User.ConsumedCalories.AsQueryable().OrderByDescending(c => c.DateAdded).GroupBy(c => c.DateAdded.Date).Select(x => new ConsumedCaloriesHistoryViewModel { Date = x.Key, EnergySumOnDate = x.Sum(e => e.Food.Nutrients.FirstOrDefault().EnergKcal / 100 * e.Amount) }).ToList();

            return View(model);
        }

        public ActionResult Reset() {

            string currentUserId = User.Identity.GetUserId();
            var currentUser = db.Users.Include(u => u.ConsumedCalories).Where(u => u.Id == currentUserId).FirstOrDefault();
            var caloriesToDelete = currentUser.ConsumedCalories.Where(c => c.DateAdded.Date == DateTime.Today.Date).ToList();
            
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