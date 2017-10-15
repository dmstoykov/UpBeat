namespace UpBeat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedusermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 30));
            CreateIndex("dbo.Albums", "User_Id");
            AddForeignKey("dbo.Albums", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Albums", "User_Id");
        }
    }
}
