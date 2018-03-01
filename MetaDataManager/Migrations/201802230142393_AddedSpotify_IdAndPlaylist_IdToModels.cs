namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSpotify_IdAndPlaylist_IdToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Spotify_Id", c => c.String());
            AddColumn("dbo.Albums", "Playlist_Id", c => c.String());
            AddColumn("dbo.Artists", "Spotify_Id", c => c.String());
            AddColumn("dbo.Songs", "Spotify_Id", c => c.String());
            AddColumn("dbo.Songs", "Playlist_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Playlist_Id");
            DropColumn("dbo.Songs", "Spotify_Id");
            DropColumn("dbo.Artists", "Spotify_Id");
            DropColumn("dbo.Albums", "Playlist_Id");
            DropColumn("dbo.Albums", "Spotify_Id");
        }
    }
}
