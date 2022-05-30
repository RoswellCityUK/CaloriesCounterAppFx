using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CaloriesCounterAppFx.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //DbSets configured for all tables.
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<FoodNutrient> FoodNutrients { get; set; }
        public DbSet<ConsumedCalories> ConsumedCaloriesTable { get; set; }
        public DbSet<ContactFormMessage> ContactFormMessages { get; set; }
        public object ApplicationUser { get; internal set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}