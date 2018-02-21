namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSongModelAndConnectedAlbumsToSongs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "AlbumName", c => c.String());
            AddColumn("dbo.Songs", "AlbumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "AlbumId");
            AddForeignKey("dbo.Songs", "AlbumId", "dbo.Albums", "Id", cascadeDelete: true);
            DropColumn("dbo.Songs", "Album");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Album", c => c.String());
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            DropColumn("dbo.Songs", "AlbumId");
            DropColumn("dbo.Songs", "AlbumName");
        }
    }
}
