namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyToSongModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "AlbumId", c => c.Int(nullable: false));
            DropColumn("dbo.Songs", "Playlist_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Playlist_Id", c => c.String());
            DropColumn("dbo.Albums", "AlbumId");
        }
    }
}
