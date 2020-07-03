namespace WcfService_CodeFirstApproach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Main1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ConfirmPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ConfirmPassword");
        }
    }
}
