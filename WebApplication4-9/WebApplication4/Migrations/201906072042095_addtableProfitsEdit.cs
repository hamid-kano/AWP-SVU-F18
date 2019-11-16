namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableProfitsEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profits", "NameUser", c => c.String());
            AddColumn("dbo.Profits", "DateTimeDownload", c => c.DateTime(nullable: false));
            DropColumn("dbo.Profits", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profits", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Profits", "DateTimeDownload");
            DropColumn("dbo.Profits", "NameUser");
        }
    }
}
