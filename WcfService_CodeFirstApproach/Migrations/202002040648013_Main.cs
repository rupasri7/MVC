namespace WcfService_CodeFirstApproach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Main : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Household", "Middlename", c => c.String());
            AlterColumn("dbo.Household", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Household", "Gender", c => c.String());
            AlterColumn("dbo.Household", "Middlename", c => c.String(nullable: false));
        }
    }
}
