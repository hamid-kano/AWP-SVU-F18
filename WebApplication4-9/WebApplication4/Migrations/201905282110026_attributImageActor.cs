namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attributImageActor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "Image");
        }
    }
}
