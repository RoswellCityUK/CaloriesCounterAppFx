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
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
        public static double GetUserCaloriesConsumedToday(string currentUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            double caloriesAmount = 0;

            var currentUser = db.Users.Include(u => u.ConsumedCalories).FirstOrDefault(x => x.Id == currentUserId);


            foreach (ConsumedCalories calories in currentUser.ConsumedCalories.AsQueryable().Where(c => c.DateAdded.Date == System.DateTime.Today.Date).ToList())
            {
                if (calories.DateAdded.Date == System.DateTime.Today.Date)
                {
                    caloriesAmount += ((double)calories.Food.Nutrients[0].EnergKcal / 100) * calories.Amount;
                }
            }
            return caloriesAmount;
        }
    }
}