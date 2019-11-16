namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableMoviesActors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviesToActors",
                c => new
                    {
                        MoviesToActorsID = c.Int(nullable: false, identity: true),
                        MoviesID = c.Int(nullable: false),
                        ActorsID = c.Int(nullable: false),
                        Movie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.MoviesToActorsID)
                .ForeignKey("dbo.Actors", t => t.ActorsID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_ID)
                .Index(t => t.ActorsID)
                .Index(t => t.Movie_ID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sight = c.String(),
                        Price = c.Int(nullable: false),
                        LinkDownload = c.String(),
                        Image = c.String(),
                        CategoryId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviesToActors", "Movie_ID", "dbo.Movies");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.Movies", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.MoviesToActors", "ActorsID", "dbo.Actors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropIndex("dbo.Movies", new[] { "CategoryId" });
            DropIndex("dbo.MoviesToActors", new[] { "Movie_ID" });
            DropIndex("dbo.MoviesToActors", new[] { "ActorsID" });
            DropTable("dbo.Directors");
            DropTable("dbo.Categories");
            DropTable("dbo.Movies");
            DropTable("dbo.MoviesToActors");
            DropTable("dbo.Actors");
        }
    }
}
