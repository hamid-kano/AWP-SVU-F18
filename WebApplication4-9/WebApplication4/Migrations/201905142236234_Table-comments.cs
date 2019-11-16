namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablecomments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentsMovies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentsMovies", "MovieID", "dbo.Movies");
            DropIndex("dbo.CommentsMovies", new[] { "MovieID" });
            DropTable("dbo.CommentsMovies");
        }
    }
}
