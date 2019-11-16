namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsUserDataMining2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Credit_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "EducationLevel", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeOwnerShaip", c => c.String());
            AddColumn("dbo.AspNetUsers", "InternetConnections", c => c.String());
            AddColumn("dbo.AspNetUsers", "MaritalStatus", c => c.String());
            AddColumn("dbo.AspNetUsers", "MovieSelector", c => c.String());
            AddColumn("dbo.AspNetUsers", "NumBathrooms", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumBedrooms", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumCars", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumChildren", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumTVs", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PPV_Freq", c => c.String());
            AddColumn("dbo.AspNetUsers", "Buying_Freq", c => c.String());
            AddColumn("dbo.AspNetUsers", "Format", c => c.String());
            AddColumn("dbo.AspNetUsers", "RentingFreq", c => c.String());
            AddColumn("dbo.AspNetUsers", "ViewigFreq", c => c.String());
            AddColumn("dbo.AspNetUsers", "TheaterFreq", c => c.String());
            AddColumn("dbo.AspNetUsers", "TV_MovieFreq", c => c.String());
            AddColumn("dbo.AspNetUsers", "TV_Signal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TV_Signal");
            DropColumn("dbo.AspNetUsers", "TV_MovieFreq");
            DropColumn("dbo.AspNetUsers", "TheaterFreq");
            DropColumn("dbo.AspNetUsers", "ViewigFreq");
            DropColumn("dbo.AspNetUsers", "RentingFreq");
            DropColumn("dbo.AspNetUsers", "Format");
            DropColumn("dbo.AspNetUsers", "Buying_Freq");
            DropColumn("dbo.AspNetUsers", "PPV_Freq");
            DropColumn("dbo.AspNetUsers", "NumTVs");
            DropColumn("dbo.AspNetUsers", "NumChildren");
            DropColumn("dbo.AspNetUsers", "NumCars");
            DropColumn("dbo.AspNetUsers", "NumBedrooms");
            DropColumn("dbo.AspNetUsers", "NumBathrooms");
            DropColumn("dbo.AspNetUsers", "MovieSelector");
            DropColumn("dbo.AspNetUsers", "MaritalStatus");
            DropColumn("dbo.AspNetUsers", "InternetConnections");
            DropColumn("dbo.AspNetUsers", "HomeOwnerShaip");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "EducationLevel");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Credit_ID");
        }
    }
}
