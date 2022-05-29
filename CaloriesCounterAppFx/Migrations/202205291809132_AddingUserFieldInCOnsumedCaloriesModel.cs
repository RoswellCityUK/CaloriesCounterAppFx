namespace CaloriesCounterAppFx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserFieldInCOnsumedCaloriesModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ConsumedCalories", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.ConsumedCalories", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ConsumedCalories", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.ConsumedCalories", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
