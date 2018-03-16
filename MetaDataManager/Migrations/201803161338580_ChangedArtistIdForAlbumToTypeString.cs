namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedArtistIdForAlbumToTypeString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "ArtistId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "ArtistId", c => c.Int(nullable: false));
        }
    }
}
