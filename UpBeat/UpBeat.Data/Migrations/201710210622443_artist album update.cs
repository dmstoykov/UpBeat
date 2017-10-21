namespace UpBeat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistalbumupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artists", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Artists", new[] { "Album_Id" });
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_Id, t.Album_Id })
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Artist_Id)
                .Index(t => t.Album_Id);
            
            DropColumn("dbo.Artists", "Album_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "Album_Id", c => c.Int());
            DropForeignKey("dbo.ArtistAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.ArtistAlbums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.ArtistAlbums", new[] { "Album_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_Id" });
            DropTable("dbo.ArtistAlbums");
            CreateIndex("dbo.Artists", "Album_Id");
            AddForeignKey("dbo.Artists", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
