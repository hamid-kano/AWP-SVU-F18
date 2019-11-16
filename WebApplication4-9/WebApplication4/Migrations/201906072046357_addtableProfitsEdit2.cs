namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableProfitsEdit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profits", "PriceMovie", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profits", "PriceMovie");
        }
    }
}
