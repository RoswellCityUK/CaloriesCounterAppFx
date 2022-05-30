namespace CaloriesCounterAppFx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNewsletterToContactForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactFormMessages", "RegisterForNewsletter", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsRegisteredForNewsletter", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsRegisteredForNewsletter");
            DropColumn("dbo.ContactFormMessages", "RegisterForNewsletter");
        }
    }
}
