namespace WcfService_CodeFirstApproach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Household",
                c => new
                    {
                        HId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 32),
                        Middlename = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Suffix = c.String(),
                        Dob = c.String(nullable: false),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.HId);
            
            CreateTable(
                "dbo.Relationship",
                c => new
                    {
                        Rid = c.Int(nullable: false, identity: true),
                        Relation = c.String(),
                    })
                .PrimaryKey(t => t.Rid);
            
            CreateTable(
                "dbo.UserHouseholdMapping",
                c => new
                    {
                        UserHouseholdMappingId = c.Int(nullable: false, identity: true),
                        HouseHoldId1 = c.Int(nullable: false),
                        HouseHoldId2 = c.Int(nullable: false),
                        RelationshipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserHouseholdMappingId)
                .ForeignKey("dbo.Household", t => t.HouseHoldId1, cascadeDelete: false)
                .ForeignKey("dbo.Household", t => t.HouseHoldId2, cascadeDelete: false)
                .ForeignKey("dbo.Relationship", t => t.RelationshipId, cascadeDelete: true)
                .Index(t => t.HouseHoldId1)
                .Index(t => t.HouseHoldId2)
                .Index(t => t.RelationshipId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRole", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserTypeId", "dbo.UserRole");
            DropForeignKey("dbo.UserHouseholdMapping", "RelationshipId", "dbo.Relationship");
            DropForeignKey("dbo.UserHouseholdMapping", "HouseHoldId2", "dbo.Household");
            DropForeignKey("dbo.UserHouseholdMapping", "HouseHoldId1", "dbo.Household");
            DropIndex("dbo.User", new[] { "UserTypeId" });
            DropIndex("dbo.UserHouseholdMapping", new[] { "RelationshipId" });
            DropIndex("dbo.UserHouseholdMapping", new[] { "HouseHoldId2" });
            DropIndex("dbo.UserHouseholdMapping", new[] { "HouseHoldId1" });
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserHouseholdMapping");
            DropTable("dbo.Relationship");
            DropTable("dbo.Household");
        }
    }
}
