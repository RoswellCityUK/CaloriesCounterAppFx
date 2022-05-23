namespace CaloriesCounterAppFx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageURL = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodCategories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.FoodNutrients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WaterG = c.Double(nullable: false),
                        EnergKcal = c.Double(nullable: false),
                        ProteinG = c.Double(nullable: false),
                        LipidTotG = c.Double(nullable: false),
                        CarbohydrtG = c.Double(nullable: false),
                        FiberTDG = c.Double(nullable: false),
                        SugarTotG = c.Double(nullable: false),
                        CalciumMg = c.Double(nullable: false),
                        IronMg = c.Double(nullable: false),
                        MagnesiumMg = c.Double(nullable: false),
                        PhosphorusMg = c.Double(nullable: false),
                        PotassiumMg = c.Double(nullable: false),
                        SodiumMg = c.Double(nullable: false),
                        ZincMg = c.Double(nullable: false),
                        CopperMg = c.Double(nullable: false),
                        ManganeseMg = c.Double(nullable: false),
                        SeleniumΜg = c.Double(nullable: false),
                        VitCMg = c.Double(nullable: false),
                        NiacinMg = c.Double(nullable: false),
                        VitB6Mg = c.Double(nullable: false),
                        VitB12Μg = c.Double(nullable: false),
                        VitAIU = c.Double(nullable: false),
                        VitARAE = c.Double(nullable: false),
                        RetinolΜg = c.Double(nullable: false),
                        AlphaCarotΜg = c.Double(nullable: false),
                        BetaCarotΜg = c.Double(nullable: false),
                        BetaCryptΜg = c.Double(nullable: false),
                        VitEMg = c.Double(nullable: false),
                        VitDΜg = c.Double(nullable: false),
                        VitDIU = c.Double(nullable: false),
                        VitKΜg = c.Double(nullable: false),
                        CholestrlMg = c.Double(nullable: false),
                        Food_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.Food_Id)
                .Index(t => t.Food_Id);
            
            CreateTable(
                "dbo.ConsumedCalories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Food_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.Food_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Food_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 150));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumedCalories", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConsumedCalories", "Food_Id", "dbo.Foods");
            DropForeignKey("dbo.FoodNutrients", "Food_Id", "dbo.Foods");
            DropForeignKey("dbo.Foods", "Category_Id", "dbo.FoodCategories");
            DropIndex("dbo.ConsumedCalories", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ConsumedCalories", new[] { "Food_Id" });
            DropIndex("dbo.FoodNutrients", new[] { "Food_Id" });
            DropIndex("dbo.Foods", new[] { "Category_Id" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.ConsumedCalories");
            DropTable("dbo.FoodNutrients");
            DropTable("dbo.Foods");
            DropTable("dbo.FoodCategories");
        }
    }
}
