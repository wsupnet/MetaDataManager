namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAlbumModelToHaveArtistId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "Artist_Id" });
            RenameColumn(table: "dbo.Albums", name: "Artist_Id", newName: "ArtistId");
            AlterColumn("dbo.Albums", "ArtistId", c => c.Int(nullable: true));
            CreateIndex("dbo.Albums", "ArtistId");
            AddForeignKey("dbo.Albums", "ArtistId", "dbo.Artists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "ArtistId" });
            AlterColumn("dbo.Albums", "ArtistId", c => c.Int());
            RenameColumn(table: "dbo.Albums", name: "ArtistId", newName: "Artist_Id");
            CreateIndex("dbo.Albums", "Artist_Id");
            AddForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists", "Id");
        }
    }
}
