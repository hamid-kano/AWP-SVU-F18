namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentsMovies", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentsMovies", "UserName");
        }
    }
}
