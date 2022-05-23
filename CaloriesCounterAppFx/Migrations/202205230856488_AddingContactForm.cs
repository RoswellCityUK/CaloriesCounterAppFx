namespace CaloriesCounterAppFx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingContactForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactFormMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false),
                        To = c.String(),
                        Subject = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        SendingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactFormMessages");
        }
    }
}
