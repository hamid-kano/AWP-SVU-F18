namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoulumnsClusteringNEW : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Channel", c => c.String());
            AddColumn("dbo.AspNetUsers", "Criteria", c => c.String());
            AddColumn("dbo.AspNetUsers", "Technology", c => c.String());
            AddColumn("dbo.AspNetUsers", "Hobbies", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Hobbies");
            DropColumn("dbo.AspNetUsers", "Technology");
            DropColumn("dbo.AspNetUsers", "Criteria");
            DropColumn("dbo.AspNetUsers", "Channel");
        }
    }
}
