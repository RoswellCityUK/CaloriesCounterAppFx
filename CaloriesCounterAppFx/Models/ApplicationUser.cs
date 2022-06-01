using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CaloriesCounterAppFx.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Class for User entity and associated data
        //Tomasz Grabowski 22/05/2022
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [StringLength(150, MinimumLength = 5)]
        public string FirstName { get; set; }
        [StringLength(150, MinimumLength = 5)]
        public string LastName { get; set; }
        public List<ConsumedCalories> ConsumedCalories { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public bool IsRegisteredForNewsletter { get; set; }
    }

    public static class ApplicationUserData
    {
        //Static class that helps retrive amount of calories consumed in particular day for top navbar and footer widgets
        //Tomasz Grabowski 22/05/2022
        public static double GetUserCaloriesConsumedToday(string currentUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            double caloriesAmount = 0;

            var currentUser = db.Users.Include(u => u.ConsumedCalories).FirstOrDefault(x => x.Id == currentUserId);

            //Querying for the Consumed Calories Table for the current User and for Today's Date
            foreach (ConsumedCalories calories in currentUser.ConsumedCalories.AsQueryable().Where(c => c.DateAdded.Date == System.DateTime.Today.Date).ToList())
            {
                if (calories.DateAdded.Date == System.DateTime.Today.Date)
                {
                    //Summing all the calories consumed by user today
                    caloriesAmount += ((double)calories.Food.Nutrients[0].EnergKcal / 100) * calories.Amount;
                }
            }
            return caloriesAmount;
        }
    }
}