
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace WcfService_CodeFirstApproach
{
    public class MainDbContext:DbContext
    {
        public MainDbContext() : base("name=DatabaseConnection")
        {
        }

        //private static DbContextOptions GetOptions()
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), "DatabaseConnection").Options;
        //}

        public DbSet<UserRole> UserRoleTable { get; set; }
        public DbSet<Relationship> RelationshipTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        public DbSet<Household> HouseholdTable { get; set; }

        public DbSet<UserHouseholdMapping> UserHouseholdMappingsTable { get; set; }
    }
}