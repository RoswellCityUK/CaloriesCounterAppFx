namespace CaloriesCounterAppFx.Migrations
{
    using CaloriesCounterAppFx.Models;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CaloriesCounterAppFx.Models.ApplicationDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CaloriesCounterAppFx.Models.ApplicationDbContext context)
        {
            /*
             * Users and Roles Seeding
             */
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!context.Users.Any())
            {
                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }
                role = roleManager.FindByName("User");
                if (role == null)
                {
                    role = new IdentityRole("User");
                    roleManager.Create(role);
                }

                // Create test users
                var user = userManager.FindByName("admin@localhost");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "admin@localhost",
                        FirstName = "Tomasz",
                        LastName = "Grabowski",
                        Email = "admin@localhost",
                        PhoneNumber = "5551234567",
                        EmailConfirmed = true
                    };
                    userManager.Create(newUser, "password1");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }
                user = userManager.FindByName("user@localhost");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "user@localhost",
                        FirstName = "Tommy",
                        LastName = "Grabowski",
                        Email = "user@localhost",
                        PhoneNumber = "5551334568",
                        EmailConfirmed = true
                    };
                    userManager.Create(newUser, "password1");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "User");
                }
                user = userManager.FindByName("person@localhost");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "person@localhost",
                        FirstName = "Katarin",
                        LastName = "Pelkov",
                        Email = "person@localhost",
                        PhoneNumber = "5551234528",
                        EmailConfirmed = true
                    };
                    userManager.Create(newUser, "password123");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "User");
                }
            }

            /*
             * Example user data seeding
             */
            if (!context.ConsumedCaloriesTable.Any())
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals("user@localhost")).Include(u => u.ConsumedCalories).First();
                List<Food> randomFoods = context.Foods.OrderBy(f => Guid.NewGuid()).Take(12).ToList();
                Random r = new Random();
                int i = 0;
                foreach(Food food in randomFoods)
                {
                    int amount = r.Next(0, 100);
                    user.ConsumedCalories.Add(new ConsumedCalories()
                    {
                        Amount = amount,
                        DateAdded = DateTime.Today.AddMinutes(amount).AddHours(i),
                        Food = food
                    });
                    i++;
                }
                context.SaveChanges();

            }

            /*
             * Food Database Seeding
             */
            if (!context.Foods.Any())
            {
                List<FoodCsv> foodFromCsvList = GetListFromCsvFile<FoodCsv>();
                Debug.WriteLine("Number of rows in the CSV File: " + foodFromCsvList.Count);
                int i = 0;
                foreach (FoodCsv fromCsv in foodFromCsvList)
                {
                    FoodCategory currentCategory = context.FoodCategories.Where(cat => cat.Name.Equals(fromCsv.Category)).FirstOrDefault();
                    if (currentCategory == null)
                    {
                        currentCategory = new FoodCategory
                        {
                            Name = fromCsv.Category
                        };
                        context.FoodCategories.Add(currentCategory);
                    }
                    Food currentFood = new Food
                    {
                        Category = currentCategory,
                        Name = fromCsv.ShrtDesc
                    };
                    context.Foods.Add(currentFood);

                    FoodNutrient currentFoodNutrient = new FoodNutrient
                    {
                        Food = currentFood,
                        WaterG = fromCsv.WaterG,
                        EnergKcal = fromCsv.EnergKcal,
                        ProteinG = fromCsv.ProteinG,
                        LipidTotG = fromCsv.LipidTotG,
                        CarbohydrtG = fromCsv.CarbohydrtG,
                        FiberTDG = fromCsv.FiberTDG,
                        SugarTotG = fromCsv.SugarTotG,
                        CalciumMg = fromCsv.CalciumMg,
                        IronMg = fromCsv.IronMg,
                        MagnesiumMg = fromCsv.MagnesiumMg,
                        PhosphorusMg = fromCsv.PhosphorusMg,
                        PotassiumMg = fromCsv.PotassiumMg,
                        SodiumMg = fromCsv.SodiumMg,
                        ZincMg = fromCsv.ZincMg,
                        CopperMg = fromCsv.CopperMg,
                        ManganeseMg = fromCsv.ManganeseMg,
                        SeleniumΜg = fromCsv.SeleniumΜg,
                        VitCMg = fromCsv.VitCMg,
                        NiacinMg = fromCsv.NiacinMg,
                        VitB6Mg = fromCsv.VitB6Mg,
                        VitB12Μg = fromCsv.VitB12Μg,
                        VitAIU = fromCsv.VitAIU,
                        VitARAE = fromCsv.VitARAE,
                        RetinolΜg = fromCsv.RetinolΜg,
                        AlphaCarotΜg = fromCsv.AlphaCarotΜg,
                        BetaCarotΜg = fromCsv.BetaCarotΜg,
                        BetaCryptΜg = fromCsv.BetaCryptΜg,
                        VitEMg = fromCsv.VitEMg,
                        VitDΜg = fromCsv.VitDΜg,
                        VitDIU = fromCsv.VitDIU,
                        VitKΜg = fromCsv.VitKΜg,
                        CholestrlMg = fromCsv.CholestrlMg
                    };
                    context.FoodNutrients.Add(currentFoodNutrient);
                    i++;
                    if (i % 100 == 0)
                    {
                        Debug.WriteLine(i);
                    }

                    context.SaveChanges();
                }
            }
        }
        public static List<T> GetListFromCsvFile<T>()
        {
            var retList = new List<T>();
            Type itemType = typeof(T);
            Type csvMap;

            string zipPath = @"..\App_Data\FoodDataCsv.zip";

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    ReadingExceptionOccurred = args =>
                    {
                        if (args.Exception is CsvHelper.FieldValidationException)
                            return false;

                        return true;
                    }
                };

                csvMap = Type.GetType(typeof(T).ToString() + "ClassMap");

                ZipArchiveEntry entry = archive.GetEntry("ABBREV.csv");

                StreamReader reader = new StreamReader(entry.Open());
                CsvReader csv = new CsvReader(reader, config);
                csv.Context.RegisterClassMap(csvMap);
                List<T> records = csv.GetRecords<T>().ToList();
                retList = records;
            }
            return retList;
        }
    }
}
